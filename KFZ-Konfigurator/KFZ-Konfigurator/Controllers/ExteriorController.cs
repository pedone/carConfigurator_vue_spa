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
    public class ExteriorController : Controller
    {
        [Route("configuration/models/model-{id}/exterior", Name = Constants.Routes.Exterior)]
        public ActionResult Index()
        {
            //TODO select default engine settings or redirect to engine settings view
            if (SessionData.ActiveConfiguration.EngineSettingsId == -1)
                throw new InvalidOperationException("Engine settings are not set");

            using (var context = new CarConfiguratorEntityContext())
            {
                var selectedId = SessionData.ActiveConfiguration.PaintId;
                var paints = context.Paints.ToList()
                    .Select(cur => new PaintViewModel(cur) { IsSelected = (cur.Id == selectedId) })
                    .OrderBy(cur => cur.Category)
                    .ToList();
                if (selectedId == -1)
                {
                    paints.First().IsSelected = true;
                    SessionData.ActiveConfiguration.PaintId = paints.First().Id;
                }

                //accessories
                var accessoryIds = SessionData.ActiveConfiguration.AccessoryIds;
                IEnumerable<AccessoryViewModel> selectedAccessories = null;
                if (accessoryIds != null && accessoryIds.Any())
                {
                    selectedAccessories = context.Accessories.Where(cur => accessoryIds.Contains(cur.Id))
                        .ToList()
                        .Select(cur => new AccessoryViewModel(cur))
                        .ToList();
                }

                // engine
                var selectedEngineSetting = new EngineSettingsViewModel(context.EngineSettings.First(cur => cur.Id == SessionData.ActiveConfiguration.EngineSettingsId));

                return View(new ExteriorPageViewModel
                {
                    Paints = paints,
                    SelectedAccessories = selectedAccessories,
                    SelectedEngineSetting = selectedEngineSetting
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetSelectedPaint(int data)
        {
            if (!Request.IsAjaxRequest())
                throw new InvalidOperationException("This action must be called with ajax");

            SessionData.ActiveConfiguration.PaintId = data;
            return new EmptyResult();
        }
    }
}
