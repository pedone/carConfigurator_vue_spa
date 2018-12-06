using KFZ_Konfigurator.Helper;
using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class EngineSettingsController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(EngineSettingsController));

        [Route("configuration/models/model-{id}/enginesettings", Name = Constants.Routes.EngineSettings)]
        public ActionResult Index(int id)
        {
            if (!SessionData.ActiveConfiguration.IsValid(id, out string error))
            {
                Log.Error(error);
                new ModelController().InitCarModel(id);
                if (!SessionData.ActiveConfiguration.IsValid(id, out string error2))
                {
                    Log.Error($"failed to initialize car model: {error2}");
                    return RedirectToRoute(Constants.Routes.ModelOverview);
                }
            }

            using (var context = new CarConfiguratorEntityContext())
            {
                return View(new EngineSettingsPageViewModel(context));
            }
        }
    }
}