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
    public class OrderOverviewController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(OrderOverviewController));

        [Route("orders", Name = Constants.Routes.OrderList)]
        public ActionResult Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                return View(new OrderPageViewModel
                {
                    Orders = context.Orders.ToList().Select(cur => new OrderViewModel(cur)).ToList()
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Delete(int id)
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
            }

            return string.Empty;
        }

        [Route("orders/load/{orderGuid}", Name = Constants.Routes.LoadOrder)]
        public ActionResult LoadOrder(string orderGuid)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var order = context.Orders.FirstOrDefault(cur => cur.Guid == orderGuid);
                if (order == null)
                    throw new ArgumentException($"configuration for order {orderGuid} not found");

                var activeConfig = SessionData.ActiveConfiguration;
                activeConfig.Reset();
                activeConfig.CarModel = new CarModelViewModel(order.Configuration.EngineSetting.CarModel);
                activeConfig.EngineSettingsId = order.Configuration.EngineSetting.Id;
                activeConfig.PaintId = order.Configuration.Paint.Id;
                activeConfig.RimId = order.Configuration.Rims.Id;
                activeConfig.AccessoryIds = order.Configuration.Accessories.Select(cur => cur.Id).ToList();
                activeConfig.ConfigurationLink = MiscHelper.GenerateConfigurationLink(Request, Url, orderGuid);
            }

            return RedirectToRoute(Constants.Routes.CurrentConfigurationOverview, new { id = SessionData.ActiveConfiguration.CarModel.Id });
        }
    }
}
