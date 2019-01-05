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
                return Json(new { accessories, engineSettings, paints, rims }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
