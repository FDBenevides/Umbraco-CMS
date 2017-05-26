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

        public HorseRequestDto() { }

        public HorseRequestDto(HorseRequest entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.MemberId = entity.MemberId;
            this.MemberName = entity.MemberName;
            //TODO: suggestions links
            //TODO: final links

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

        [Column(Name = "ageRange")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string AgeRange { get; set; }

        [Column(Name = "size")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Size { get; set; }

        [Column(Name = "destination")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Destination { get; set; }

        [Column(Name = "priceRange")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string PriceRange { get; set; }

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

        [ResultColumn]
        public List<HorseRequestLinkDto> HorseLinks { get; set; }

        [ResultColumn]
        public int NumOfRequests { get; set; }
        #endregion
    }
}