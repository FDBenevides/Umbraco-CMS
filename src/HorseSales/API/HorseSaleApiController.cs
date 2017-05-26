using HorseSales.Models;
using HorseSales.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;


namespace Umbraco.Web.UI.Controllers
{
    [PluginController("LW")]
    public class HorseSalesApiController : UmbracoAuthorizedJsonController
    {
        public IEnumerable<HorseRequest> GetAll()
        {
            HorseRequestDatabase hrdb = new HorseRequestDatabase(DatabaseContext);
            return hrdb.GetAll();
        }

        public IEnumerable<HorseRequest> GetRequestsGroupByMember()
        {
            HorseRequestDatabase hrdb = new HorseRequestDatabase(DatabaseContext);
            return hrdb.GetRequestsGroupByMember();
        }

        public IEnumerable<HorseRequest> GetAllByMemberId(string memberId)
        {
            HorseRequestDatabase hrdb = new HorseRequestDatabase(DatabaseContext);
            return hrdb.GetAllByMemberId(memberId);
        }

        public HorseRequest GetById(int id)
        {
            HorseRequestDatabase hrdb = new HorseRequestDatabase(DatabaseContext);
            return hrdb.GetBydId(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The newly record's ID</returns>
        public HorseRequest Create(HorseRequest request)
        {
            var memberId = 0;
            if (!int.TryParse(request.MemberId,out memberId))
                throw new Exception("A Member ID shoud be provided to create a request");

            /// 1st - retrieve the Member Name using the Umbraco.Core.MemberService
            var memberModel = Members.GetById(memberId);
            if(memberModel == null)
                throw new Exception("A valid Member ID is required to create a request");

            request.MemberName = memberModel.Name;
            /// it probably won't never happen...
            if (string.IsNullOrWhiteSpace(request.MemberName))
            {
                request.MemberName = request.MemberId;
            }
            ///checks if a name/title for the request was given
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                request.Name = string.Format("{0}_{1}", request.MemberId, DateTime.Now.ToString("yyyyMMddHHmmss"));
            }

            /// 2nd - save the request in the database
            HorseRequestDatabase hrdb = new HorseRequestDatabase(DatabaseContext);
            var savedRequest = hrdb.Create(request);

            return savedRequest;
        }

        public HorseRequest PostSave (HorseRequest request)
        {
            HorseRequestDatabase hrdb = new HorseRequestDatabase(DatabaseContext);
            var savedRequest = hrdb.Update(request);
            return savedRequest;

            //var db = DatabaseContext.Database;

            //if (request.Id > 0){
            //    db.Update(request);
            //}else{
            //    db.Save(request);
            //}

            //return request;
        }

        public int DeleteById(int id)
        {
            HorseRequestDatabase hrdb = new HorseRequestDatabase(DatabaseContext);
            var result = hrdb.DeleteById(id);

            if (result == 0)
                throw new Exception("Error deleting request");

            return result;
            //var db = DatabaseContext.Database;
            //return db.Delete<HorseRequest>(id);
        }
    }
}