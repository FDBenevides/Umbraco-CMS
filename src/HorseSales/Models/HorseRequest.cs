using HorseSales.Json;
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

namespace HorseSales.Models
{
    public class HorseRequest
    {
        #region Constructors
        public HorseRequest() {  }
        #endregion

        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string MemberId { get; set; }

        public string MemberName { get; set; }

        public string AgeMin { get; set; }
        public string AgeMax { get; set; }

        public string SizeMin { get; set; }
        public string SizeMax { get; set; }

        public string Destination { get; set; }

        public string PriceMin { get; set; }
        public string PriceMax { get; set; }

        public string Goal { get; set; }

        public string CoatColor { get; set; }

        public string Gender { get; set; }

        public string OtherDetails { get; set; }

        public string Status { get; set; }

        public string Discipline { get; set; }

        public string TeachingLevel { get; set; }
        public string TeachingLevelAux { get; set; }

        public string Temperament { get; set; }
        public string TemperamentAux { get; set; }

        public string PiroFree{ get; set; }

        [JsonConverter(typeof(LinkPickerJsonConverter))]
        public LinkPickerList HorseLinksObj
        {
            get {
                return _horseLinksObj;
            }

            set {
                _horseLinksObj = value;
            }
        }
        private LinkPickerList _horseLinksObj;

        [JsonConverter(typeof(LinkPickerJsonConverter))]
        public LinkPickerList FinalHorseLinksObj
        {
            get
            {
                return _finalHorseLinksObj;
            }

            set
            {
                _finalHorseLinksObj = value;
            }
        }
        private LinkPickerList _finalHorseLinksObj;

        public int NumOfRequests { get; set; }

        public bool HasChildren
        {
            get
            {
                return NumOfRequests > 0;
            }
        }

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
                HorseRequestProperty propAgeMin = HorseRequestProperty.GenerateProperty("AgeMin", this.AgeMin);
                HorseRequestProperty propAgeMax = HorseRequestProperty.GenerateProperty("AgeMin", this.AgeMax);
                HorseRequestProperty propGender = HorseRequestProperty.GenerateProperty("Gender", this.Gender);
                HorseRequestProperty propSizeMin = HorseRequestProperty.GenerateProperty("SizeMin", this.SizeMin);
                HorseRequestProperty propSizeMax = HorseRequestProperty.GenerateProperty("SizeMax", this.SizeMax);
                HorseRequestProperty propPriceMin = HorseRequestProperty.GenerateProperty("PriceMin", this.PriceMin);
                HorseRequestProperty propPriceMax = HorseRequestProperty.GenerateProperty("PriceMax", this.PriceMax);
                HorseRequestProperty propDestination = HorseRequestProperty.GenerateProperty("Destination", this.Destination);
                HorseRequestProperty propGoal = HorseRequestProperty.GenerateProperty("Goal", this.Goal);
                HorseRequestProperty propOther = HorseRequestProperty.GenerateProperty("OtherDetails", this.OtherDetails);
                HorseRequestProperty propDiscipline = HorseRequestProperty.GenerateProperty("Discipline", this.Discipline);
                HorseRequestProperty propPiroFree = HorseRequestProperty.GenerateProperty("PiroFree", this.PiroFree);
                HorseRequestProperty propStatus = HorseRequestProperty.GenerateProperty("Status", this.Status);
                HorseRequestProperty propTeachingLevel = HorseRequestProperty.GenerateProperty("TeachingLevel", this.TeachingLevel);
                HorseRequestProperty propTeachingLevelAux = HorseRequestProperty.GenerateProperty("TeachingLevelAux", this.TeachingLevelAux);
                HorseRequestProperty propTemperament = HorseRequestProperty.GenerateProperty("Temperament", this.Temperament);
                HorseRequestProperty propTemperamentAux = HorseRequestProperty.GenerateProperty("TemperamentAux", this.TemperamentAux);

                properties.Add(propId);
                properties.Add(propMemberId);
                properties.Add(propCoatColor);
                properties.Add(propAgeMin);
                properties.Add(propAgeMax);
                properties.Add(propGender);
                properties.Add(propSizeMin);
                properties.Add(propSizeMax);
                properties.Add(propPriceMin);
                properties.Add(propPriceMax);
                properties.Add(propDestination);
                properties.Add(propGoal);
                properties.Add(propOther);
                properties.Add(propDiscipline);
                properties.Add(propPiroFree);
                properties.Add(propStatus);
                properties.Add(propTeachingLevel);
                properties.Add(propTeachingLevelAux);
                properties.Add(propTemperament);
                properties.Add(propTemperamentAux);

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
            return MemberName + " (" + MemberId + ") - " + NumOfRequests;
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
                    return new HorseRequestProperty("textbox", value, name, name, false, string.Empty, false);
                case "HorseLinks":
                    return new HorseRequestProperty("/App_Plugins/Skybrud.LinkPicker/Views/LinkPicker.html", value, "HorseLinksObj", name, false, string.Empty, false);
                case "FinalHorseLinks":
                    return new HorseRequestProperty("/App_Plugins/Skybrud.LinkPicker/Views/LinkPicker.html", value, "FinalHorseLinksObj", name, false, string.Empty, false);
                case "Goal":
                    return new HorseRequestProperty("textarea", value, name, name, false, string.Empty, false);
                case "OtherDetails":
                    return new HorseRequestProperty("textarea", value, name, name, false, string.Empty, false);
                case "Status":
                    return new HorseRequestProperty("readonlyvalue", value, name, name, false, string.Empty, false);
                default:
                    return new HorseRequestProperty("textbox", value, name, name, false, string.Empty, false);
            }
        }

        #endregion

    }
}