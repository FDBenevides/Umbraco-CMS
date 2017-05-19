using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Web.UI.App_Plugins.LW.Objects;

namespace Umbraco.Web.UI.App_Plugins.LW.Events
{
    public class RegisterEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var db = applicationContext.DatabaseContext.Database;
            var schemaHelper = new DatabaseSchemaHelper(db, LoggerResolver.Current.Logger, applicationContext.DatabaseContext.SqlSyntax);

            if (!schemaHelper.TableExist("HorseRequest"))
            {
                schemaHelper.CreateTable<HorseRequest>(false);
            }
        }
    }
}