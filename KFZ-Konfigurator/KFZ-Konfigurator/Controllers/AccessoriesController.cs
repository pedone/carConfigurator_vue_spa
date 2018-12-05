using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class AccessoriesController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(AccessoriesController));

        [Route("configuration/models/model-{id}/accessories", Name = Constants.Routes.Accessories)]
        public ActionResult Index(int id)
        {
            if (!SessionData.ActiveConfiguration.IsValid(id, out string error))
            {
                Log.Error(error);
                return RedirectToRoute(Constants.Routes.ModelOverview);
            }

            using (var context = new CarConfiguratorEntityContext())
            {
                var accessoryCategories = context.Categories.OfType<AccessoryCategory>().Select(cur => cur.Name).ToList();
                return View(new AccessoriesPageViewModel(context) { AccessoryCategories = accessoryCategories });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string SetSelectedAccessories(string id)
        {
            if (!Request.IsAjaxRequest())
            {
                Log.Error("SetSelectedAccessories was called without ajax");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return "This action must be called with ajax";
            }

            if (id == null)
            {
                SessionData.ActiveConfiguration.AccessoryIds = null;
            }
            else
            {
                var ids = id.Trim('[', ']').Split(',').Select(cur => int.Parse(cur)).ToList();
                SessionData.ActiveConfiguration.AccessoryIds = ids.ToArray();
            }
            Log.Debug($"AccessoryIds set to {id}");
            return string.Empty; ;
        }
    }
}
