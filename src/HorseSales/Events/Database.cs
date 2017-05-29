using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using HorseSales.Models;
using HorseSales.Persistence;

namespace HorseSales.Events
{
    public class Database : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var db = applicationContext.DatabaseContext.Database;
            var schemaHelper = new DatabaseSchemaHelper(db, LoggerResolver.Current.Logger, applicationContext.DatabaseContext.SqlSyntax);

            if (!schemaHelper.TableExist("HorseRequest"))
            {
                schemaHelper.CreateTable<HorseRequest>(false);
            }
            if (!schemaHelper.TableExist("HorseRequestDto"))
            {
                schemaHelper.CreateTable<HorseRequestDto>(false);
            }
            if (!schemaHelper.TableExist("HorseRequestLinkDto"))
            {
                schemaHelper.CreateTable<HorseRequestLinkDto>(false);
            }
            if (!schemaHelper.TableExist("HorseRequestLinkCommentDto"))
            {
                schemaHelper.CreateTable<HorseRequestLinkCommentDto>(false);
            }
        }
    }
}