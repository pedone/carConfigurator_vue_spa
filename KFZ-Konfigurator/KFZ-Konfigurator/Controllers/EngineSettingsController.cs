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
                return RedirectToRoute(Constants.Routes.ModelOverview);
            }

            using (var context = new CarConfiguratorEntityContext())
            {
                var carModel = context.CarModels.First(cur => cur.Id == id);
                //engine settings
                var selectedId = SessionData.ActiveConfiguration.EngineSettingsId;
                var settings = carModel.EngineSettings.ToList()
                    .Select(cur => new EngineSettingsViewModel(cur) { IsSelected = (cur.Id == selectedId) })
                    .OrderBy(cur => cur.Price)
                    .ToList();

                //accessories
                var accessoryIds = SessionData.ActiveConfiguration.AccessoryIds;
                IEnumerable<AccessoryViewModel> selectedAccessories = null;
                if (accessoryIds.Any())
                {
                    selectedAccessories = context.Accessories.Where(cur => accessoryIds.Contains(cur.Id))
                        .ToList()
                        .Select(cur => new AccessoryViewModel(cur))
                        .ToList();
                }

                //paint
                PaintViewModel selectedPaint = new PaintViewModel(context.Paints.First(cur => cur.Id == SessionData.ActiveConfiguration.PaintId));

                //rims
                RimViewModel selectedRims = new RimViewModel(context.Rims.First(cur => cur.Id == SessionData.ActiveConfiguration.RimId));

                return View(new EngineSettingsPageViewModel
                {
                    EngineSettings = settings,
                    SelectedAccessories = selectedAccessories,
                    SelectedPaint = selectedPaint,
                    SelectedRims = selectedRims
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetSelectedEngineSettings(int id)
        {
            if (!Request.IsAjaxRequest())
                throw new InvalidOperationException("This action must be called with ajax");

            SessionData.ActiveConfiguration.EngineSettingsId = id;
            return new EmptyResult();
        }

        [Route("configuration/load/{guid}", Name = Constants.Routes.LoadConfiguration)]
        public ActionResult LoadConfiguration(string guid)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var configuration = context.Configurations.FirstOrDefault(cur => cur.Guid == guid);
                if (configuration == null)
                    throw new ArgumentException("configuration not found");

                var activeConfig = SessionData.ActiveConfiguration;
                activeConfig.Reset();
                activeConfig.CarModel = new CarModelViewModel(configuration.EngineSetting.CarModel);
                activeConfig.EngineSettingsId = configuration.EngineSetting.Id;
                activeConfig.PaintId = configuration.Paint.Id;
                activeConfig.RimId = configuration.Rims.Id;
                activeConfig.AccessoryIds = configuration.Accessories.Select(cur => cur.Id).ToList();
                activeConfig.ConfigurationLink = new ConfigurationLink
                {
                    Url = MiscHelper.GenerateConfigurationLink(Request, Url, guid),
                    Id = configuration.Id
                };
            }

            return RedirectToRoute(Constants.Routes.EngineSettings, new { id = SessionData.ActiveConfiguration.CarModel.Id });
        }
    }
}