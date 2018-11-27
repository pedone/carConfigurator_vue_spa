using KFZ_Konfigurator.Helper;
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
                if (accessoryIds.Any())
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
                    Rims = rims,
                    ConfigurationLink = SessionData.ActiveConfiguration.ConfigurationLink
                });
            }
        }

        [HttpPost]
        public string GenerateConfigurationLink()
        {
            if (!SessionData.ActiveConfiguration.IsValid(null, out string error))
            {
                Log.Error(error);
                return error;
            }
            if (!string.IsNullOrEmpty(SessionData.ActiveConfiguration.ConfigurationLink))
            {
                return SessionData.ActiveConfiguration.ConfigurationLink;
            }

            var guid = MiscHelper.GenerateShortGuid();
            var config = SessionData.ActiveConfiguration;
            using (var context = new CarConfiguratorEntityContext())
            {
                var configuration = context.Configurations.Create();
                configuration.Guid = guid;
                configuration.EngineSetting = context.EngineSettings.First(cur => cur.Id == config.EngineSettingsId);
                configuration.Paint = context.Paints.First(cur => cur.Id == config.PaintId);
                configuration.Rims = context.Rims.First(cur => cur.Id == config.RimId);
                configuration.Accessories = context.Accessories.Where(cur => config.AccessoryIds.Contains(cur.Id)).ToList();

                context.Configurations.Add(configuration);
                context.SaveChanges();
            }

            config.ConfigurationLink = MiscHelper.GenerateConfigurationLink(Request, Url, guid);
            return config.ConfigurationLink;
        }
    }
}