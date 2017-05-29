using HorseSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace HorseSales.Persistence
{
    [TableName("HorseRequestLinkDto")]
    [PrimaryKey("linkId", autoIncrement = true)]
    [ExplicitColumns]
    public class HorseRequestLinkDto
    {
        #region Constructors
        public HorseRequestLinkDto() {
            Comments = new List<HorseRequestLinkCommentDto>();
        }

        #endregion

        #region Properties
        [PrimaryKeyColumn(AutoIncrement = true)]
        [Column(Name = "linkId")]
        public int LinkId { get; set; }

        [Column(Name = "id")]
        public int Id { get; set; }

        [Column(Name = "requestId")]
        [ForeignKey(typeof(HorseRequestDto))]
        public int RequestId { get; set; }

        [Column(Name = "type")]
        public string Type { get; set; }

        [Column(Name = "mode")]
        public string Mode { get; set; }

        [Column(Name = "name")]
        public string Name { get; set; }

        [Column(Name = "url")]
        public string Url { get; set; }

        [Column(Name = "target")]
        public string Target { get; set; }

        [Column(Name = "ref")]
        public string Ref { get; set; }

        [Column(Name = "price")]
        public string Price { get; set; }

        [Column(Name = "video")]
        public string Video { get; set; }

        [ResultColumn]
        public List<HorseRequestLinkCommentDto> Comments { get; set; }

        #endregion

    }
}