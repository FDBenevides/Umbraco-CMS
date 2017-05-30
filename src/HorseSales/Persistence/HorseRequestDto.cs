using HorseSales.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace HorseSales.Persistence
{
    [TableName("HorseRequestDto")]
    [PrimaryKey("id", autoIncrement = true)]
    [ExplicitColumns]
    public class HorseRequestDto
    {
        #region Constructors

        public HorseRequestDto() {
            HorseLinks = new List<HorseRequestLinkDto>();
        }
        #endregion

        #region Properties
        [Column(Name = "id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column(Name = "name")]
        public string Name { get; set; }

        [Column(Name = "memberId")]
        public string MemberId { get; set; }

        [Column(Name = "memberName")]
        public string MemberName { get; set; }

        [Column(Name = "ageMin")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string AgeMin { get; set; }

        [Column(Name = "ageMax")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string AgeMax { get; set; }

        [Column(Name = "sizeMin")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string SizeMin { get; set; }

        [Column(Name = "sizeMax")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string SizeMax { get; set; }

        [Column(Name = "destination")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Destination { get; set; }

        [Column(Name = "priceMin")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string PriceMin { get; set; }

        [Column(Name = "priceMax")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string PriceMax { get; set; }

        [Column(Name = "goal")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Goal { get; set; }

        [Column(Name = "coatColor")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string CoatColor { get; set; }

        [Column(Name = "gender")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Gender { get; set; }

        [Column(Name = "otherDetails")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string OtherDetails { get; set; }

        [Column(Name = "status")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Status { get; set; }

        [Column(Name = "discipline")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Discipline { get; set; }

        [Column(Name = "teachingLevel")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string TeachingLevel { get; set; }

        [Column(Name = "teachingLevelAux")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string TeachingLevelAux { get; set; }

        [Column(Name = "temperament")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Temperament { get; set; }

        [Column(Name = "temperamentAux")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string TemperamentAux { get; set; }

        [Column(Name = "piroFree")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string PiroFree { get; set; }

        [ResultColumn]
        public List<HorseRequestLinkDto> HorseLinks { get; set; }

        [ResultColumn]
        public int NumOfRequests { get; set; }
        #endregion
    }
}