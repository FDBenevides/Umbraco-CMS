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
    [PrimaryKey("id", autoIncrement = true)]
    [ExplicitColumns]
    public class HorseRequestLinkDto
    {
        #region Constructors
        public HorseRequestLinkDto() { }

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

        #endregion

    }
}