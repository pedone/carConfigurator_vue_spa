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
        
        private int CalculatePageCount(int itemCount)
        {
            return (int)Math.Ceiling((double)itemCount / Constants.PageItemsCount); ;
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

                return Json(new { accessories, engineSettings, paints, rims, accessoryCategories, paintCategories }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
