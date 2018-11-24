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
                var selectedCarModel = context.CarModels.FirstOrDefault(cur => cur.Id == id);
                if (selectedCarModel == null)
                    throw new ArgumentException($"Car model with id {id} was not found in database");

                var existingCarModel = SessionData.ActiveConfiguration.CarModel;
                if (existingCarModel == null)
                {
                    SessionData.ActiveConfiguration.CarModel = new CarModelViewModel(selectedCarModel);
                }
                else if (existingCarModel.Id != selectedCarModel.Id)
                {
                    // if the car model changed, clear the last configuration
                    SessionData.ActiveConfiguration.Reset();
                    SessionData.ActiveConfiguration.CarModel = new CarModelViewModel(selectedCarModel);
                }

                var selectedId = SessionData.ActiveConfiguration.EngineSettings?.Id ?? -1;
                var settings = context.EngineSettings.ToList()
                    .Where(cur => cur.CarModel.Id == id)
                    .Select(cur => new EngineSettingsViewModel(cur) { IsSelected = (cur.Id == selectedId) })
                    .OrderBy(cur => cur.Price)
                    .ToList();
                if (selectedId == -1)
                    settings.First().IsSelected = true;

                return View(settings);
            }
        }
    }
}