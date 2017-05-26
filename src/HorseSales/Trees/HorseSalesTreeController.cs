using System;
using System.Net.Http.Formatting;
using umbraco;
using uCore = Umbraco.Core;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Web.WebApi.Filters;
using System.Collections.Generic;
using HorseSales.API;
using Umbraco.Web.UI.Controllers;
using Umbraco.Web;
using umbraco.BusinessLogic.Actions;

namespace HorseSales.Trees
{
    [Tree("horseSales", "horseSalesTree", "Horse Requests")]
    [PluginController("LW")]
    [UmbracoApplicationAuthorize("horseSales")]
    public class HorseSalesTreeController : TreeController
    {

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var ctrl = new HorseSalesApiController();
            var nodes = new TreeNodeCollection();

            if (id == uCore.Constants.System.Root.ToInvariantString())
            {
                foreach (var request in ctrl.GetRequestsGroupByMember())
                {
                    var node = CreateTreeNode("member" + request.MemberId.ToString(), "-1", queryStrings, request.GroupByMemberToString(), "icon-umb-users", request.HasChildren,
                                queryStrings.GetValue<string>("application") + TreeAlias.EnsureStartsWith('/') + "/viewMember/" + request.MemberId
                                );
                    nodes.Add(node);
                }
            }
            else if (id.InvariantContains("member"))
            {
                var numberId = id.Replace("member", "");
                foreach (var request in ctrl.GetAllByMemberId(numberId))
                {
                    var node = CreateTreeNode(request.Id.ToString(), id, queryStrings, request.Name, "icon-coin");
                    nodes.Add(node);
                }
            }

            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            if (id == uCore.Constants.System.Root.ToInvariantString())
            {
                menu.Items.Add<ActionNew>("New Request","actionRoute", queryStrings.GetValue<string>("application") + TreeAlias.EnsureStartsWith('/') + "/create/-1");
                //menu.Items.Add<ActionNew>("Additional Data", false, new Dictionary<string, object>() { { "testKey", "testValue" } });

                //menu.Items.Add<CreateChildEntity, ActionNew>(ui.Text("actions", ActionNew.Instance.Alias));
                //menu.Items.Add<CreateChildEntity, ActionNew>("Additional Data", false, new Dictionary<string, object>() { { "testKey", "testValue" } });
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias), true);
            }
            else if (id.InvariantContains("member"))
            {
                var numberId = id.Replace("member", "");
                menu.Items.Add<ActionNew>("New Request for Member", "actionRoute", queryStrings.GetValue<string>("application") + TreeAlias.EnsureStartsWith('/') + "/create/" + numberId);
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias), true);
            }else
            {
                menu.Items.Add<ActionDelete>(ui.Text("actions", ActionDelete.Instance.Alias), true);
            }
            return menu;
        }
    }
}