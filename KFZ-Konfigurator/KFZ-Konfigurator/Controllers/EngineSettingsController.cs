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
            var existingSelection = SessionData.ActiveConfiguration.Get<EngineSettingsViewModel>().FirstOrDefault();
            using (var context = new CarConfiguratorEntityContext())
            {
                var settings = context.EngineSettings.ToList()
                    .Where(cur => cur.CarModel.Id == id)
                    .Select(cur => new EngineSettingsViewModel(cur))
                    .OrderBy(cur => cur.Price)
                    .ToList();
                var itemSelectionSource = new ViewModelSelection<EngineSettingsViewModel>(settings, new[] { existingSelection ?? settings.First() });
                return View(itemSelectionSource);
            }
        }
    }
}