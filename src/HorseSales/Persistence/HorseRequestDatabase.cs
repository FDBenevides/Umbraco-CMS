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

        #region protected methods
        protected HorseRequest BuildEntity(HorseRequestDto dto)
        {
            var suggestionsLinks = new List<LinkPickerItem>();
            var finalLinks = new List<LinkPickerItem>();
            if (dto.HorseLinks != null)
            {
                suggestionsLinks.AddRange(dto.HorseLinks.
                                    Where(x => x.Type.InvariantEquals("suggestions")).
                                    Select(x => new LinkPickerItem(x.Id, x.Type, x.Type, x.Type, LinkPickerMode.Url)));

                finalLinks.AddRange(dto.HorseLinks.
                                    Where(x => x.Type.InvariantEquals("final")).
                                    Select(x => new LinkPickerItem(x.Id, x.Type, x.Type, x.Type, LinkPickerMode.Url)));
            }

            var entity = new HorseRequest()
            {
                Id = dto.Id,
                Name = dto.Name,
                MemberId = dto.MemberId,
                MemberName = dto.MemberName,
                CoatColor = dto.CoatColor,
                HorseLinksObj = new LinkPickerList() { Items = suggestionsLinks.ToArray() },
                FinalHorseLinksObj = new LinkPickerList() { Items = finalLinks.ToArray() },
                NumOfRequests = dto.NumOfRequests
            };

            return entity;
        }

        public HorseRequestDto BuildDto(HorseRequest entity)
        {
            List<HorseRequestLinkDto> links = new List<HorseRequestLinkDto>();

            if ((entity.HorseLinksObj !=null) && entity.HorseLinksObj.HasItems)
            {
                links.AddRange(entity.HorseLinksObj.Items.Select(link => new HorseRequestLinkDto()
                {
                    LinkId = link.LinkId,
                    Id = link.Id,
                    RequestId = entity.Id,
                    Type = "suggestions"
                }));
            }

            if ((entity.FinalHorseLinksObj !=null) && entity.FinalHorseLinksObj.HasItems)
            {
                links.AddRange(entity.HorseLinksObj.Items.Select(link => new HorseRequestLinkDto()
                {
                    LinkId = link.LinkId,
                    Id = link.Id,
                    RequestId = entity.Id,
                    Type = "final"
                }));
            }

            var dto = new HorseRequestDto
            {
                Id = entity.Id,
                Name = entity.Name,
                MemberId = entity.MemberId,
                MemberName = entity.MemberName,
                AgeRange = entity.AgeRange,
                CoatColor = entity.CoatColor,
                Destination = entity.Destination,
                Gender = entity.Gender,
                PriceRange = entity.PriceRange,
                Size = entity.Size,
                Goal = entity.Goal,
                OtherDetails = entity.OtherDetails,
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

        public HorseRequest GetBydId(int requestId)
        {
            var sql = new Sql();
            sql.Select("*")
                .From<HorseRequestDto>(SqlSyntax)
                .LeftJoin<HorseRequestLinkDto>(SqlSyntax)
                .On<HorseRequestDto, HorseRequestLinkDto>(SqlSyntax, left => left.Id, right => right.RequestId)
                .Where<HorseRequestDto>(req => req.Id.Equals(requestId), SqlSyntax)
                //MUST be ordered by this GUID ID for the HorseRequestLinkRelator to work
                .OrderBy<HorseRequestDto>(dto => dto.Id, SqlSyntax);

            var db = DatabaseContext.Database;
            var dtos = db.Fetch<HorseRequestDto, HorseRequestLinkDto, HorseRequestDto>(new HorseRequestLinkRelator().Map, sql);

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
            db.Save(dto);

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
