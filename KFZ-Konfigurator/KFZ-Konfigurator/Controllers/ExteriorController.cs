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
    public class ExteriorController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ExteriorController));

        [Route("configuration/models/model-{id}/exterior", Name = Constants.Routes.Exterior)]
        public ActionResult Index(int id)
        {
            if (!SessionData.ActiveConfiguration.IsValid(id, out string error))
            {
                Log.Error(error);
                return RedirectToRoute(Constants.Routes.ModelOverview);
            }

            using (var context = new CarConfiguratorEntityContext())
            {
                var paintCategories = context.Categories.OfType<PaintCategory>().Select(cur => cur.Name).ToList();
                return View(new ExteriorPageViewModel(context) { PaintCategories = paintCategories });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string SetSelectedPaintAndRim(int paintId, int rimId)
        {
            if (!Request.IsAjaxRequest())
            {
                Log.Error("SetSelectedPaintAndRim was called without ajax");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return "This action must be called with ajax";
            }

            SessionData.ActiveConfiguration.PaintId = paintId;
            SessionData.ActiveConfiguration.RimId = rimId;
            return string.Empty;
        }
    }
}
