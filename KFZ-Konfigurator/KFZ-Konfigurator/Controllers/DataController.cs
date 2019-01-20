using KFZ_Konfigurator.Helper;
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
    public class DataController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(DataController));

        [HttpGet]
        public JsonResult CarModelList()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var result = context.CarModels.ToList().Select(cur => new CarModelViewModel(cur)).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult OrderList()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var result = new
                {
                    PageCount = CalculatePageCount(context.Orders.Count()),
                    Orders = context.Orders.Take(Constants.PageItemsCount).ToList()
                    .Select(cur => new OrderViewModel(cur))
                    .ToList()
                };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public JsonResult LoadOrder(string guid)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var order = context.Orders.FirstOrDefault(cur => cur.Guid == guid);
                if (order == null)
                    throw new ArgumentException($"order {guid} was not found");

                return Json(new OrderOverviewPageViewModel(order, false), JsonRequestBehavior.AllowGet);
            }
        }

        private int CalculatePageCount(int itemCount)
        {
            return (int)Math.Ceiling((double)itemCount / Constants.PageItemsCount); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteOrder(int id, int pageIndex)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var toDelete = context.Orders.Find(id);
                if (toDelete != null)
                {
                    toDelete.Configuration.Accessories.Clear();
                    context.Orders.Remove(toDelete);
                    context.SaveChanges();
                }

                //return the item that's next in line because everything after the deleted item is moving up
                var newOrderItem = context.Orders.ToList().ElementAtOrDefault(pageIndex * Constants.PageItemsCount + (Constants.PageItemsCount - 1));
                return Json(new
                {
                    NewPageCount = CalculatePageCount(context.Orders.Count()),
                    NewItem = (newOrderItem != null ? new OrderViewModel(newOrderItem) : null)
                });
            }
        }

        [HttpGet]
        public JsonResult LoadOrderPage(int pageIndex)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var orders = context.Orders.ToList().Skip(Constants.PageItemsCount * pageIndex).Take(Constants.PageItemsCount).ToList()
                .Select(cur => new OrderViewModel(cur))
                .ToList();
                return Json(orders, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult ConfigurationData(int carModelId)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var carModel = context.CarModels.First(cur => cur.Id == carModelId);

                //accessories
                var accessories = context.Accessories.ToList()
                    .Select(cur => new AccessoryViewModel(cur))
                    .OrderBy(cur => cur.Price)
                    .ToList();

                //engine settings
                var engineSettings = carModel.EngineSettings.ToList()
                    .Select(cur => new EngineSettingsViewModel(cur))
                    .OrderBy(cur => cur.Price)
                    .ToList();

                //paints
                var paints = context.Paints.ToList()
                    .Select(cur => new PaintViewModel(cur))
                    .OrderBy(cur => cur.Name)
                    .ToList();

                //rims
                var rims = context.Rims.ToList()
                        .Select(cur => new RimViewModel(cur))
                        .ToList();

                //accessoryCategories
                var accessoryCategories = context.Categories.OfType<AccessoryCategory>().Select(cur => cur.Name).ToList();
                //paintCategories
                var paintCategories = context.Categories.OfType<PaintCategory>().Select(cur => cur.Name).ToList();

                return Json(new
                {
                    model = new
                    {
                        series = carModel.SeriesCategory.Name,
                        bodyType = carModel.BodyCategory.Name,
                        year = carModel.Year
                    },
                    accessories,
                    engineSettings,
                    paints,
                    rims,
                    accessoryCategories,
                    paintCategories
                }, JsonRequestBehavior.AllowGet);
            }
        }
        private Configuration InitConfiguration(CarConfiguratorEntityContext context, (int engineSettings, int[] accessories, int paint, int rims) data)
        {
            var configuration = context.Configurations.Create();
            configuration.EngineSetting = context.EngineSettings.First(cur => cur.Id == data.engineSettings);
            configuration.Paint = context.Paints.First(cur => cur.Id == data.paint);
            configuration.Rims = context.Rims.First(cur => cur.Id == data.rims);
            if (data.accessories != null)
                configuration.Accessories = context.Accessories.Where(cur => data.accessories.Contains(cur.Id)).ToList();

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
        public string PlaceOrder(string description, int[] accessories, int engineSettings, int paint, int rims)
        {
            if (!Request.IsAjaxRequest())
            {
                Log.Error("PlaceOrder was called without ajax");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return "This action must be called with ajax";
            }

            Configuration configuration;
            using (var context = new CarConfiguratorEntityContext())
            {
                //store the current configuration
                configuration = InitConfiguration(context, (engineSettings, accessories, paint, rims));
                context.Configurations.Add(configuration);

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

                return newOrder.Guid;
            }
        }
    }
}
