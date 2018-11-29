using KFZ_Konfigurator.Helper;
using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class ConfigurationsController : Controller
    {
        [Route("configurations", Name = Constants.Routes.ConfigurationList)]
        public ActionResult Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                return View(context.Configurations.ToList().Select(cur => new ConfigurationViewModel(cur)).ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var configToDelete = context.Configurations.Find(id);
                if (configToDelete != null)
                {
                    if (configToDelete.Orders.Count > 0)
                        throw new InvalidOperationException("configuration has orders and can't be deleted");

                    configToDelete.Accessories.Clear();
                    context.Configurations.Remove(configToDelete);
                    context.SaveChanges();
                }
            }

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
