using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KFZ_Konfigurator.Controllers
{
    public class EngineSettingsController : Controller
    {
        public ActionResult Index()
        {
            if (WebApiApplication.ActiveConfiguration.CarModelId == null)
                return RedirectToAction("Index", "Configuration");

            using (var context = new CarConfiguratorEntityContext())
            {
                var selectedModelId = WebApiApplication.ActiveConfiguration.CarModelId;
                return View(context.EngineSettings.ToList().Where(cur => cur.CarModel.Id == selectedModelId).Select(cur => new EngineSettingsViewModel(cur)).ToList());
            }
        }
    }
}