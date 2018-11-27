using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class ConfigurationOverviewController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConfigurationOverviewController));

        [Route("configuration/models/model-{id}/overview", Name = Constants.Routes.ConfigurationOverview)]
        public ActionResult Index(int id)
        {
            if (!SessionData.ActiveConfiguration.IsValid(id, out string error))
            {
                Log.Error(error);
                return RedirectToRoute(Constants.Routes.ModelOverview);
            }

            using (var context = new CarConfiguratorEntityContext())
            {
                var carModel = context.CarModels.First(cur => cur.Id == id);

                //engine settings
                var engineSettings = new EngineSettingsViewModel(context.EngineSettings.First(cur => cur.Id == SessionData.ActiveConfiguration.EngineSettingsId));

                //accessories
                var accessoryIds = SessionData.ActiveConfiguration.AccessoryIds;
                IEnumerable<AccessoryViewModel> accessories = null;
                if (accessoryIds != null && accessoryIds.Any())
                {
                    accessories = context.Accessories.Where(cur => accessoryIds.Contains(cur.Id))
                        .ToList()
                        .Select(cur => new AccessoryViewModel(cur))
                        .ToList();
                }

                //paint
                PaintViewModel paint = new PaintViewModel(context.Paints.First(cur => cur.Id == SessionData.ActiveConfiguration.PaintId));

                //rims
                RimViewModel rims = new RimViewModel(context.Rims.First(cur => cur.Id == SessionData.ActiveConfiguration.RimId));

                return View(new ConfigurationOverviewPageViewModel
                {
                    EngineSetting = engineSettings,
                    Accessories = accessories,
                    Paint = paint,
                    Rims = rims
                });
            }
        }
    }
}