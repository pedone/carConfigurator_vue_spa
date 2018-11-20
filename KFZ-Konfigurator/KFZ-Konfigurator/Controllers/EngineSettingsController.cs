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
        [Route("configuration/models/model-{id}/enginesettings", Name = Constants.Routes.EngineSettings)]
        public ActionResult Index(int id)
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                return View(context.EngineSettings.ToList().Where(cur => cur.CarModel.Id == id).Select(cur => new EngineSettingsViewModel(cur)).ToList());
            }
        }
    }
}