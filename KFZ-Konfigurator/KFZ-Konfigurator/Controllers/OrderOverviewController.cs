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

        [Route("orders/view/{orderGuid}", Name = Constants.Routes.ViewOrder)]
        public ActionResult Index(string orderGuid)
        {
            return View(BuildViewModel(orderGuid, false));
        }

        [Route("orders/view/{orderGuid}/orderplaced", Name = Constants.Routes.ViewOrderAfterPlaced)]
        public ActionResult OrderPlaced(string orderGuid)
        {
            return View("/Views/OrderOverview/Index.cshtml", BuildViewModel(orderGuid, true));
        }

        private OrderOverviewPageViewModel BuildViewModel(string orderGuid, bool orderJustPlaced)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var order = context.Orders.FirstOrDefault(cur => cur.Guid == orderGuid);
                if (order == null)
                    throw new ArgumentException($"order {orderGuid} was not found");

                return new OrderOverviewPageViewModel(order, MiscHelper.GenerateOrderLink(Request, Url, order.Guid), orderJustPlaced);
            }
        }
    }
}
