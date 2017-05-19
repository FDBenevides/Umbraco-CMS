using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Skybrud.LinkPicker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Umbraco.Web.UI.App_Plugins.LW.Objects
{
    [TableName("HorseRequest")]
    [ExplicitColumns]
    public class HorseRequest
    {
        #region Constructors
        public HorseRequest()
        {

        }
        #endregion

        #region Properties
        [Column(Name = "Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }

        [Column(Name = "MemberId")]
        public string MemberId { get; set; }

        [Column(Name = "MemberName")]
        public string MemberName { get; set; }

        [Column(Name = "AgeRange")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string AgeRange { get; set; }

        [Column(Name = "Size")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Size { get; set; }

        [Column(Name = "Destination")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Destination { get; set; }

        [Column(Name = "PriceRange")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string PriceRange { get; set; }

        [Column(Name = "Goal")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Goal { get; set; }

        [Column(Name = "CoatColor")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string CoatColor { get; set; }

        [Column(Name = "OtherDetails")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string OtherDetails { get; set; }

        [Column(Name = "HorseLinks")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [NullSetting(NullSetting = NullSettings.Null)]      
        public string HorseLinks
        {
            get
            {
                return _horseLinks;
            }
            set
            {
                _horseLinks = value;
            }
        }
        private string _horseLinks;

        [Ignore]
        public LinkPickerList HorseLinksObj
        {
            get {
                if(_horseLinksObj != null)
                {
                    return _horseLinksObj;
                }else if(_horseLinks != null)
                {
                    return LinkPickerList.Parse(JObject.Parse(_horseLinks));
                }
                return new LinkPickerList();
            }

            set {
                _horseLinksObj = value;
            }
        }
        private LinkPickerList _horseLinksObj;


        [Column(Name = "FinalHorseLinks")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string FinalHorseLinks
        {
            get
            {
                return _finalHorseLinks;
            }
            set
            {
                _finalHorseLinks = value;
            }
        }
        private string _finalHorseLinks;

        [Ignore]
        public LinkPickerList FinalHorseLinksObj
        {
            get
            {
                if (_finalHorseLinksObj != null)
                {
                    return _finalHorseLinksObj;
                }
                else if (_finalHorseLinks != null)
                {
                    return LinkPickerList.Parse(JObject.Parse(_finalHorseLinks));
                }
                return new LinkPickerList();
            }

            set
            {
                _finalHorseLinksObj = value;
            }
        }
        private LinkPickerList _finalHorseLinksObj;


        [ResultColumn]
        public int NumOfRequests { get; set; }


        [IgnoreAttribute()]
        public bool HasChildren
        {
            get
            {
                return NumOfRequests > 0;
            }
        }

        [IgnoreAttribute()]
        public Dictionary<string, List<HorseRequestProperty>> Tabs
        {
            get
            {
                var tabs = new Dictionary<string, List<HorseRequestProperty>>();

                #region Tab1
                List<HorseRequestProperty> properties = new List<HorseRequestProperty>();
                tabs.Add("tab1", properties);

                HorseRequestProperty propId = HorseRequestProperty.GenerateProperty("Id", this.Id.ToString());
                HorseRequestProperty propMemberId = HorseRequestProperty.GenerateProperty("MemberId", this.MemberId);
                HorseRequestProperty propCoatColor = HorseRequestProperty.GenerateProperty("CoatColor", this.CoatColor);
                HorseRequestProperty propGoal = HorseRequestProperty.GenerateProperty("Goal", this.Goal);
                HorseRequestProperty propDestination = HorseRequestProperty.GenerateProperty("Destination", this.Destination);

                properties.Add(propId);
                properties.Add(propMemberId);
                properties.Add(propCoatColor);
                properties.Add(propGoal);
                properties.Add(propDestination);

                #endregion

                #region Tab 2
                properties = new List<HorseRequestProperty>();
                tabs.Add("tab2", properties);
                HorseRequestProperty propLinks = HorseRequestProperty.GenerateProperty("HorseLinks", this.HorseLinksObj);
                properties.Add(propLinks);
                #endregion

                #region Tab 3
                properties = new List<HorseRequestProperty>();
                tabs.Add("tab3", properties);
                HorseRequestProperty propFinalLinks = HorseRequestProperty.GenerateProperty("FinalHorseLinks", this.FinalHorseLinksObj);
                properties.Add(propFinalLinks);

                #endregion
                return tabs;
            }
        }

        #endregion

        #region Methods
        public string GroupByMemberToString()
        {
            return MemberId + " (" + NumOfRequests + ")";
        }


        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion

    }

    public class HorseRequestProperty
    {
        #region Properties
        public string description;

        public bool hideLabel;

        public string label;

        public string propertyAlias;

        public object value;

        public string view;

        public bool loadResource;
        #endregion

        #region Constructors 
        public HorseRequestProperty(string view, object value, string propertyAlias, string label, bool hideLabel, string description, bool loadResource)
        {
            this.view = view;
            this.value = value;
            this.propertyAlias = propertyAlias;
            this.label = label;
            this.hideLabel = hideLabel;
            this.description = description;
            this.loadResource = loadResource;
        }
        #endregion

        #region Static Methods
        public static HorseRequestProperty GenerateProperty(string name, object value)
        {
            switch (name)
            {
                case "Id":
                    return new HorseRequestProperty("readonlyvalue", value, name, name, false, string.Empty, false);
                case "MemberId":
                    return new HorseRequestProperty("readonlyvalue", value, name, name, false, string.Empty, false);
                case "CoatColor":
                    return new HorseRequestProperty("Dropdown Coat Color", value, name, name, false, string.Empty, true);
                case "HorseLinks":
                    return new HorseRequestProperty("/App_Plugins/Skybrud.LinkPicker/Views/LinkPicker.html", value, "HorseLinksObj", name, false, string.Empty, false);
                case "FinalHorseLinks":
                    return new HorseRequestProperty("/App_Plugins/Skybrud.LinkPicker/Views/LinkPicker.html", value, "FinalHorseLinksObj", name, false, string.Empty, false);
                case "Goal":
                    return new HorseRequestProperty("textarea", value, name, name, false, string.Empty, false);
                default:
                    return new HorseRequestProperty("textbox", value, name, name, false, string.Empty, false);
            }
        }

        #endregion

    }
}