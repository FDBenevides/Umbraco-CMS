using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using Umbraco.Web.UI.App_Plugins.LW.Objects;

namespace Umbraco.Web.UI.Controllers
{
    [PluginController("LW")]
    public class HorseSalesApiController : UmbracoAuthorizedJsonController
    {

        public IEnumerable<HorseRequest> GetAll()
        {
            var db = DatabaseContext.Database;
            var query = new Sql().Select("[MemberId], count(Id) as NumOfRequests").From("HorseRequest").GroupBy("MemberId");
            return db.Fetch<HorseRequest>(query);
        }

        public IEnumerable<HorseRequest> GetAllByMemberId(string memberId)
        {
            var db = DatabaseContext.Database;
            var query = new Sql().Select("*").From("HorseRequest").Where<HorseRequest>(x => x.MemberId == memberId, DatabaseContext.SqlSyntax);
            return db.Fetch<HorseRequest>(query);
        }

        public HorseRequest GetById(int id)
        {
            var db = DatabaseContext.Database;
            var query = new Sql().Select("*").From("HorseRequest").Where<HorseRequest>(x => x.Id == id, DatabaseContext.SqlSyntax);
            return db.Fetch<HorseRequest>(query).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The newly record's ID</returns>
        public HorseRequest Create(HorseRequest request)
        {
            var db = DatabaseContext.Database;


            ////TODO: 
            /// first, retrieve the Member Name using the Umbraco.Core.MemberService
            /// ....
            /// second, save the request in the database
            if (string.IsNullOrWhiteSpace(request.MemberName))
            {
                request.MemberName = request.MemberId;
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                request.Name = string.Format("{0}_{1}", request.MemberId, DateTime.Now.ToString("yyyyMMddHHmmss"));
            }

            db.Save(request);

            return request;
        }

        public HorseRequest PostSave (HorseRequest request)
        {
            var db = DatabaseContext.Database;

            if (request.Id > 0){
                db.Update(request);
            }else{
                db.Save(request);
            }

            return request;
        }

        public int DeleteById(int id)
        {
            var db = DatabaseContext.Database;
            return db.Delete<HorseRequest>(id);
        }
    }
}