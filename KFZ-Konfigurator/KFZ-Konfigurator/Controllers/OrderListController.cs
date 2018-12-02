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
    public class OrderListController : Controller
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(OrderListController));

        [Route("orders", Name = Constants.Routes.OrderList)]
        public ActionResult Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var pageCount = CalculatePageCount(context.Orders.Count());
                return View(new OrderListPageViewModel
                {
                    PageCount = pageCount,
                    Orders = context.Orders.Take(Constants.PageItemsCount).ToList()
                    .Select(cur => new OrderViewModel(cur, Url.RouteUrl(Constants.Routes.ViewOrder, new { orderGuid = cur.Guid })))
                    .ToList()
                });
            }
        }

        private int CalculatePageCount(int itemCount)
        {
            return (int)Math.Ceiling((double)itemCount / Constants.PageItemsCount); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int id, int pageIndex)
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
                    NewItem = (newOrderItem != null ? new OrderViewModel(newOrderItem, Url.RouteUrl(Constants.Routes.ViewOrder, new { orderGuid = newOrderItem.Guid })) : null)
                });
            }
        }

        [HttpGet]
        public JsonResult LoadPage(int pageIndex)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                var orders = context.Orders.ToList().Skip(Constants.PageItemsCount * pageIndex).Take(Constants.PageItemsCount).ToList()
                .Select(cur => new OrderViewModel(cur, Url.RouteUrl(Constants.Routes.ViewOrder, new { orderGuid = cur.Guid })))
                .ToList();
                return Json(orders, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
