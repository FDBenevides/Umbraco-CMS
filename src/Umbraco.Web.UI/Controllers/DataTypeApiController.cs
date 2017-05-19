using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

using AutoMapper;

using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web.Editors;
using Umbraco.Web.Models.ContentEditing;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using Umbraco.Web.UI.Cache;

namespace Umbraco.Web.UI.Controllers
{
    [PluginController("LW")]
    public class DataTypeApiController : UmbracoAuthorizedJsonController
    {
        /// <summary>
        /// The get by name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="DataTypeDisplay"/>.
        /// </returns>       
        public object GetByName(string name)
        {
            var all = DataTypeCacheProvider.Current.GetOrExecute(() => this.Services.DataTypeService.GetAllDataTypeDefinitions().ToList());
            var dataType = all.FirstOrDefault(x => x.Name == name);
            return this.FormatDataType(dataType);
        }

        /// <summary>
        /// The format data type.
        /// </summary>
        /// <param name="dtd">
        /// The dtd.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        /// <exception cref="HttpResponseException">
        /// </exception>
        protected object FormatDataType(IDataTypeDefinition dtd)
        {
            if (dtd == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var dataTypeDisplay = Mapper.Map<IDataTypeDefinition, DataTypeDisplay>(dtd);
            var propEditor = PropertyEditorResolver.Current.GetByAlias(dtd.PropertyEditorAlias);

            var configDictionairy = new Dictionary<string, object>();

            foreach (var pv in dataTypeDisplay.PreValues)
            {
                configDictionairy.Add(pv.Key, pv.Value);
            }

            return new
            {
                guid = dtd.Key,
                propertyEditorAlias = dtd.PropertyEditorAlias,
                config = configDictionairy,
                view = propEditor.ValueEditor.View
            };
        }
    }
}