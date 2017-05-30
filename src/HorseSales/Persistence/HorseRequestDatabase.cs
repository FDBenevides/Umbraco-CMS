using HorseSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Querying;
using Umbraco.Core.Persistence.SqlSyntax;

namespace HorseSales.Persistence
{
    class HorseRequestDatabase
    {
        public DatabaseContext DatabaseContext { get; private set; }
        public ISqlSyntaxProvider SqlSyntax { get; private set; }

        #region constructors
        public HorseRequestDatabase(DatabaseContext dbContext)
        {
            DatabaseContext = dbContext;
            SqlSyntax = dbContext.SqlSyntax;
        }
        #endregion

        #region Build Entity methods
        protected List<LinkPickerItemComment> BuildLinkCommentsEntity(IEnumerable<HorseRequestLinkCommentDto> commentsDto)
        {
            var commentsList = new List<LinkPickerItemComment>();

            if (commentsDto != null)
            {
                foreach (var commentDto in commentsDto)
                {
                    var comment = new LinkPickerItemComment(commentDto.Id, commentDto.Author,
                                                            commentDto.Datetime, commentDto.Text);
                    commentsList.Add(comment);
                }
            }

            return commentsList;
        }

        protected List<LinkPickerItem> BuildLinkEntity(IEnumerable<HorseRequestLinkDto> linksDto)
        {
            var linkList = new List<LinkPickerItem>();

            foreach (var linkDto in linksDto)
            {
                var comments = BuildLinkCommentsEntity(linkDto.Comments);

                var link = new LinkPickerItem(linkDto.LinkId, linkDto.Id, linkDto.Name, linkDto.Url,
                                              linkDto.Target, linkDto.Mode,
                                              comments.ToArray(),
                                              linkDto.Ref, linkDto.Price, linkDto.Video);

                linkList.Add(link);
            }

            return linkList;
        }

        protected HorseRequest BuildEntity(HorseRequestDto dto)
        {
            var suggestionsLinks = new List<LinkPickerItem>();
            var finalLinks = new List<LinkPickerItem>();
            if ((dto.HorseLinks != null) && (dto.HorseLinks.Count > 0))
            {
                var suggestionsDto = dto.HorseLinks.Where(x => x.Type.InvariantEquals("suggestions"));
                var finalDto = dto.HorseLinks.Where(x => x.Type.InvariantEquals("final"));

                suggestionsLinks = BuildLinkEntity(suggestionsDto);
                finalLinks = BuildLinkEntity(finalDto);
            }

            var entity = new HorseRequest()
            {
                Id = dto.Id,
                Name = dto.Name,
                MemberId = dto.MemberId,
                MemberName = dto.MemberName,
                CoatColor = dto.CoatColor,
                AgeMax = dto.AgeMax,
                AgeMin = dto.AgeMin,
                Destination = dto.Destination,
                Gender = dto.Gender,
                Goal = dto.Goal,
                OtherDetails = dto.OtherDetails,
                PriceMax = dto.PriceMax,
                PriceMin = dto.PriceMin,
                SizeMax = dto.SizeMax,
                SizeMin = dto.SizeMin,
                Discipline = dto.Discipline,
                PiroFree = dto.PiroFree,
                Status = dto.Status,
                TeachingLevel = dto.TeachingLevel,
                TeachingLevelAux = dto.TeachingLevelAux,
                Temperament = dto.Temperament,
                TemperamentAux = dto.TemperamentAux,

                HorseLinksObj = new LinkPickerList() { Items = suggestionsLinks.ToArray() },
                FinalHorseLinksObj = new LinkPickerList() { Items = finalLinks.ToArray() },
                NumOfRequests = dto.NumOfRequests
            };

            return entity;
        }
        #endregion

        #region Build DTO methods

        protected List<HorseRequestLinkCommentDto> BuildRequestLinkCommentsDto(LinkPickerItemComment[] items, int linkId)
        {
            var comments = new List<HorseRequestLinkCommentDto>();

            foreach (var linkComment in items)
            {
                var comment = new HorseRequestLinkCommentDto()
                {
                    LinkId = linkId,
                    Id = linkComment.Id,
                    Author = linkComment.Author,
                    Text = linkComment.Text,
                    Datetime = linkComment.Datetime
                };

                comments.Add(comment);
            }

            return comments;
        }

        protected List<HorseRequestLinkDto> BuildRequestLinksDto(LinkPickerItem[] items, int requestId, string type)
        {
            List<HorseRequestLinkDto> links = new List<HorseRequestLinkDto>();

            foreach (var item in items)
            {
                var link = new HorseRequestLinkDto()
                {
                    LinkId = item.LinkId,
                    Id = item.Id,
                    RequestId = requestId,
                    Type = type,
                    Mode = item.Mode.ToString(),
                    Name = item.Name,
                    Target = item.Target,
                    Url = item.Url,
                    Price = item.Price,
                    Ref = item.Ref,
                    Video = item.Video,
                    Comments = new List<HorseRequestLinkCommentDto>()
                };

                if ((item.Comments != null) && (item.Comments.Length > 0))
                {
                    ///add comment to the linkDto
                    link.Comments.AddRange(BuildRequestLinkCommentsDto(item.Comments, item.LinkId));
                }

                links.Add(link);
            }

            return links;
        }

        protected HorseRequestDto BuildDto(HorseRequest entity)
        {
            List<HorseRequestLinkDto> links = new List<HorseRequestLinkDto>();

            if ((entity.HorseLinksObj != null) && entity.HorseLinksObj.HasItems)
            {
                ///add suggestions to the list of links
                links.AddRange(BuildRequestLinksDto(entity.HorseLinksObj.Items, entity.Id, "suggestions"));
            }

            if ((entity.FinalHorseLinksObj != null) && entity.FinalHorseLinksObj.HasItems)
            {
                ///add final links to the list of links
                links.AddRange(BuildRequestLinksDto(entity.FinalHorseLinksObj.Items, entity.Id, "final"));
            }

            var dto = new HorseRequestDto
            {
                Id = entity.Id,
                Name = entity.Name,
                MemberId = entity.MemberId,
                MemberName = entity.MemberName,
                AgeMin = entity.AgeMin,
                AgeMax = entity.AgeMax,
                CoatColor = entity.CoatColor,
                Destination = entity.Destination,
                Gender = entity.Gender,
                PriceMin = entity.PriceMin,
                PriceMax = entity.PriceMax,
                SizeMin = entity.SizeMin,
                SizeMax = entity.SizeMax,
                Goal = entity.Goal,
                OtherDetails = entity.OtherDetails,
                Discipline = entity.Discipline,
                PiroFree = entity.PiroFree,
                Status = entity.Status,
                TeachingLevel = entity.TeachingLevel,
                TeachingLevelAux = entity.TeachingLevelAux,
                Temperament = entity.Temperament,
                TemperamentAux = entity.TemperamentAux,
                HorseLinks = links
            };

            return dto;
        }
        #endregion

        #region public methods

        public IEnumerable<HorseRequest> GetRequestsGroupByMember()
        {
            var sql = new Sql();
            sql.Select("[memberId],[memberName], count(id) as NumOfRequests")
                .From<HorseRequestDto>(SqlSyntax)
                .GroupBy("[memberId]", "[memberName]");
            //GroupBy<HorseRequestDto>(x => x.MemberId, SqlSyntax);

            var db = DatabaseContext.Database;
            var dtos = db.Fetch<HorseRequestDto>(sql);
            //var result = dtos.Select(factory.BuildEntity);

            var result = dtos.Select(x => BuildEntity(x)).ToList();

            return result;
        }


        public IEnumerable<HorseRequest> GetAll()
        {
            var sql = new Sql();
            sql.Select("*")
                .From<HorseRequestDto>(SqlSyntax)
                .LeftJoin<HorseRequestLinkDto>(SqlSyntax)
                .On<HorseRequestDto, HorseRequestLinkDto>(SqlSyntax, left => left.Id, right => right.RequestId)
                //MUST be ordered by this GUID ID for the HorseRequestLinkRelator to work
                .OrderBy<HorseRequestDto>(dto => dto.Id, SqlSyntax);
            var db = DatabaseContext.Database;
            var dtos = db.Fetch<HorseRequestDto, HorseRequestLinkDto, HorseRequestDto>(new HorseRequestLinkRelator().Map, sql);

            var result = dtos.Select(x => BuildEntity(x)).ToList();

            return result;
        }


        public IEnumerable<HorseRequest> GetAllByMemberId(string memberId)
        {
            var sql = new Sql();
            sql.Select("*")
                .From<HorseRequestDto>(SqlSyntax)
                .LeftJoin<HorseRequestLinkDto>(SqlSyntax)
                .On<HorseRequestDto, HorseRequestLinkDto>(SqlSyntax, left => left.Id, right => right.RequestId)
                .Where<HorseRequestDto>(req => req.MemberId.Equals(memberId), SqlSyntax)
                //MUST be ordered by this GUID ID for the HorseRequestLinkRelator to work
                .OrderBy<HorseRequestDto>(dto => dto.Id, SqlSyntax);

            var db = DatabaseContext.Database;
            var dtos = db.Fetch<HorseRequestDto, HorseRequestLinkDto, HorseRequestDto>(new HorseRequestLinkRelator().Map, sql);

            var result = dtos.Select(x => BuildEntity(x)).ToList();

            return result;
        }


        /// <summary>
        /// Retrieves a detailed request by Id.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns>The request including all related links and link comments</returns>
        public HorseRequest GetBydId(int requestId)
        {
            var sql = new Sql();
            sql.Select("*")
                .From<HorseRequestDto>(SqlSyntax)
                .LeftJoin<HorseRequestLinkDto>(SqlSyntax)
                .On<HorseRequestDto, HorseRequestLinkDto>(SqlSyntax, left => left.Id, right => right.RequestId)
                .LeftJoin<HorseRequestLinkCommentDto>(SqlSyntax)
                .On<HorseRequestLinkDto, HorseRequestLinkCommentDto>(SqlSyntax, left => left.LinkId, right => right.LinkId)
                .Where<HorseRequestDto>(req => req.Id.Equals(requestId), SqlSyntax)
                //MUST be ordered by this GUID ID for the HorseRequestLinkRelator to work
                .OrderBy<HorseRequestDto>(dto => dto.Id, SqlSyntax);

            var db = DatabaseContext.Database;
            //var dtos = db.Fetch<HorseRequestDto, HorseRequestLinkDto, HorseRequestDto>(new HorseRequestLinkRelator().Map, sql);
            var dtos = db.Fetch<HorseRequestDto, HorseRequestLinkDto, HorseRequestLinkCommentDto, HorseRequestDto>(new HorseRequestLinkCommentRelator().Map, sql);

            var result = dtos.Select(x => BuildEntity(x)).First();
            return result;
        }

        public HorseRequest Create(HorseRequest request)
        {
            var newDto = BuildDto(request);

            var db = DatabaseContext.Database;
            db.Save(newDto);

            var entity = GetBydId(newDto.Id);
            return entity;
        }

        public HorseRequest Update(HorseRequest request)
        {
            var dto = BuildDto(request);

            var db = DatabaseContext.Database;
            db.Update(dto);

            foreach (var link in dto.HorseLinks)
            {
                foreach (var linkComment in link.Comments)
                {
                    db.Save(linkComment);
                }
                db.Save(link);
            }
            foreach (var toDeleteId in request.HorseLinksObj.ToDelete)
            {
                //the cast is to force to go to the desired function specification 'Delete<T>(object pocoOrPrimaryKey)'
                db.Delete<HorseRequestLinkDto>((object)toDeleteId);
            }
            foreach (var toDeleteId in request.FinalHorseLinksObj.ToDelete)
            {
                //the cast is to force to go to the desired function specification 'Delete<T>(object pocoOrPrimaryKey)'
                db.Delete<HorseRequestLinkDto>((object)toDeleteId);
            }

            var entity = BuildEntity(dto);
            return entity;
        }

        public int DeleteById(int id)
        {
            var db = DatabaseContext.Database;
            return db.Delete<HorseRequestDto>(id);
        }
        #endregion
    }
}
