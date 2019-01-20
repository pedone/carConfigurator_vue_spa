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
    //[Route("api/[controller]/[action]")]
    public class DataController : Controller
    {
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
                    .Select(cur => new OrderViewModel(cur, Url.RouteUrl(Constants.Routes.ViewOrder, new { orderGuid = cur.Guid })))
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

                return Json(new OrderOverviewPageViewModel(order, "", false), JsonRequestBehavior.AllowGet);
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
                    NewItem = (newOrderItem != null ? new OrderViewModel(newOrderItem, Url.RouteUrl(Constants.Routes.ViewOrder, new { orderGuid = newOrderItem.Guid })) : null)
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
                    model = new {
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
    }
}
