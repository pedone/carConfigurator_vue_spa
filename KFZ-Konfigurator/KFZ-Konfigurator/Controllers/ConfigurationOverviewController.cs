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
                new ModelController().InitCarModel(id);
                if (!SessionData.ActiveConfiguration.IsValid(id, out string error2))
                {
                    Log.Error($"failed to initialize car model: {error2}");
                    return RedirectToRoute(Constants.Routes.ModelOverview);
                }
            }

            using (var context = new CarConfiguratorEntityContext())
            {
                return View(new ConfigurationOverviewPageViewModel(context));
            }
        }

        private Configuration InitConfiguration(CarConfiguratorEntityContext context)
        {
            var configViewModel = SessionData.ActiveConfiguration;
            var configuration = context.Configurations.Create();
            configuration.EngineSetting = context.EngineSettings.First(cur => cur.Id == configViewModel.EngineSettingsId);
            configuration.Paint = context.Paints.First(cur => cur.Id == configViewModel.PaintId);
            configuration.Rims = context.Rims.First(cur => cur.Id == configViewModel.RimId);
            configuration.Accessories = context.Accessories.Where(cur => configViewModel.AccessoryIds.Contains(cur.Id)).ToList();

            return configuration;
        }

        private (double basePrice, double extrasPrice) CalculatePrice(Configuration config)
        {
            var accessoryPrices = config.Accessories.Select(cur => cur.Price).ToList();
            var extras = config.Paint.Price +
                config.Rims.Price +
                (accessoryPrices.Count > 0 ? accessoryPrices.Aggregate((result, next) => result + next) : 0);

            return (basePrice: config.EngineSetting.Price, extrasPrice: extras);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string PlaceOrder(string description)
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
                try
                {
                    //store the current configuration
                    configuration = InitConfiguration(context);
                    context.Configurations.Add(configuration);

                }
                catch (ArgumentException ex)
                {
                    Log.Error(ex);
                    Response.StatusCode = (int)HttpStatusCode.Conflict;
                    return ex.Message;
                }

                var newOrder = context.Orders.Create();
                newOrder.Description = description;
                newOrder.Guid = MiscHelper.GenerateShortGuid();
                newOrder.DateTime = DateTime.Now;

                (double basePrice, double extrasPrice) configurationPrice = CalculatePrice(configuration);
                newOrder.BasePrice = configurationPrice.basePrice;
                newOrder.ExtrasPrice = configurationPrice.extrasPrice;

                newOrder.Configuration = configuration;
                context.Orders.Add(newOrder);
                context.SaveChanges();

                Log.Info($"configuration for order {newOrder.Id} was created");
                Log.Info($"a new order with id '{newOrder.Id}' was successfully placed");

                SessionData.ActiveConfiguration.OrderLink = Url.RouteUrl(Constants.Routes.ViewOrderAfterPlaced, new { orderGuid = newOrder.Guid });
            }

            return SessionData.ActiveConfiguration.OrderLink;
        }
    }
}