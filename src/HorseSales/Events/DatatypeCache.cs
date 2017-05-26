using HorseSales.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace HorseSales.Events
{
    public class DatatypeCache : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            DataTypeService.Saved += DataTypeService_Saved;
            DataTypeService.Deleted += DataTypeService_Deleted;
        }

        void DataTypeService_Deleted(IDataTypeService sender, Umbraco.Core.Events.DeleteEventArgs<Umbraco.Core.Models.IDataTypeDefinition> e)
        {
            DataTypeCacheProvider.Current.Clear();
        }

        void DataTypeService_Saved(IDataTypeService sender, Umbraco.Core.Events.SaveEventArgs<Umbraco.Core.Models.IDataTypeDefinition> e)
        {
            DataTypeCacheProvider.Current.Clear();
        }
    }
}
