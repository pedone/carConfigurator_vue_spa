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
    public class ConfigurationController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                return View(context.CarModels.ToList().Select(cur => new CarModelViewModel(cur)).ToList());
            }
        }

        public ActionResult Model(int id)
        {
            WebApiApplication.ActiveConfiguration.CarModelId = id;
            return RedirectToAction("Index", "EngineSettings");
        }
    }
}
