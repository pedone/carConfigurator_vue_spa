using KFZ_Konfigurator.Helper;
using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class ConfigurationOverviewController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConfigurationOverviewController));

        [Route("configuration/models/model-{id}/overview", Name = Constants.Routes.CurrentConfigurationOverview)]
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
                    Accessories = accessories ?? Enumerable.Empty<AccessoryViewModel>(),
                    Paint = paint,
                    Rims = rims,
                    ConfigurationLink = SessionData.ActiveConfiguration.ConfigurationLink?.Url
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string GenerateConfigurationLink(string configurationName)
        {
            if (!Request.IsAjaxRequest())
            {
                Log.Error("GenerateConfigurationLink was called without ajax");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return "This action must be called with ajax";
            }
            if (!SessionData.ActiveConfiguration.IsValid(null, out string error))
            {
                Log.Error(error);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return error;
            }
            if (SessionData.ActiveConfiguration.HasConfiguration)
            {
                return SessionData.ActiveConfiguration.ConfigurationLink.Url;
            }

            Configuration configuration;
            using (var context = new CarConfiguratorEntityContext())
            {
                try
                {
                    configuration = InitConfiguration(context, configurationName);
                    context.Configurations.Add(configuration);
                    context.SaveChanges();
                    Log.Info($"configuration {configurationName} ({configuration.Id}) was created");
                }
                catch (ArgumentException ex)
                {
                    Log.Error(ex);
                    Response.StatusCode = (int)HttpStatusCode.Conflict;
                    return ex.Message;
                }
            }

            SessionData.ActiveConfiguration.ConfigurationLink = new ConfigurationLink
            {
                Url = MiscHelper.GenerateConfigurationLink(Request, Url, configuration.Guid),
                Id = configuration.Id
            };
            return SessionData.ActiveConfiguration.ConfigurationLink.Url;
        }

        private Configuration InitConfiguration(CarConfiguratorEntityContext context, string name)
        {
            if (context.Configurations.Any(cur => cur.Name == name))
                throw new ArgumentException($"a configuration with the name {name} already exists");

            var configViewModel = SessionData.ActiveConfiguration;
            var configuration = context.Configurations.Create();
            configuration.Name = name;
            configuration.Guid = MiscHelper.GenerateShortGuid();
            configuration.EngineSetting = context.EngineSettings.First(cur => cur.Id == configViewModel.EngineSettingsId);
            configuration.Paint = context.Paints.First(cur => cur.Id == configViewModel.PaintId);
            configuration.Rims = context.Rims.First(cur => cur.Id == configViewModel.RimId);
            configuration.Accessories = context.Accessories.Where(cur => configViewModel.AccessoryIds.Contains(cur.Id)).ToList();
            configuration.Price = CalculateConfigurationPrice(configuration);

            return configuration;
        }

        private double CalculateConfigurationPrice(Configuration config)
        {
            var accessoryPrices = config.Accessories.Select(cur => cur.Price).ToList();
            return config.EngineSetting.Price +
                config.Paint.Price +
                config.Rims.Price +
                (accessoryPrices.Count > 0 ? accessoryPrices.Aggregate((result, next) => result + next) : 0);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string PlaceOrder(string configurationName)
        {
            if (!Request.IsAjaxRequest())
            {
                Log.Error("PlaceOrder was called without ajax");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return "This action must be called with ajax";
            }
            if (!SessionData.ActiveConfiguration.IsValid(null, out string error))
            {
                Log.Error(error);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return error;
            }

            Configuration configuration;
            using (var context = new CarConfiguratorEntityContext())
            {
                if (!SessionData.ActiveConfiguration.HasConfiguration)
                {
                    try
                    {
                        //store the current configuration
                        configuration = InitConfiguration(context, configurationName);
                        context.Configurations.Add(configuration);

                    }
                    catch (ArgumentException ex)
                    {
                        Log.Error(ex);
                        Response.StatusCode = (int)HttpStatusCode.Conflict;
                        return ex.Message;
                    }
                }
                else
                {
                    //TODO update the current configuration
                    //the configuration was already generated, get the existing one
                    var activeConfigId = SessionData.ActiveConfiguration.ConfigurationLink.Id;
                    configuration = context.Configurations.FirstOrDefault(cur => cur.Id == activeConfigId);
                    if (configuration == null)
                    {
                        var message = "the active configuration was not found in the database";
                        Log.Error(message);
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return message;
                    }
                }

                var newOrder = context.Orders.Create();
                newOrder.Configuration = configuration;
                context.Orders.Add(newOrder);
                context.SaveChanges();

                Log.Info($"configuration {configurationName} ({configuration.Id}) was created");
                Log.Info($"a new order with id {newOrder.Id} was successfully placed");

                SessionData.ActiveConfiguration.ConfigurationLink = new ConfigurationLink
                {
                    Url = MiscHelper.GenerateConfigurationLink(Request, Url, configuration.Guid),
                    Id = configuration.Id
                };
            }

            return SessionData.ActiveConfiguration.ConfigurationLink.Url;
        }
    }
}