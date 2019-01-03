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
    }
}
