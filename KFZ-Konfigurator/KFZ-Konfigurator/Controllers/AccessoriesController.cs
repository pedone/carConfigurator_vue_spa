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
    public class AccessoriesController : Controller
    {
        [Route("configuration/models/model-{id}/accessories", Name = Constants.Routes.Accessories)]
        public ActionResult Index()
        {
            //TODO select default engine settings or redirect to engine settings view
            if (SessionData.ActiveConfiguration.EngineSettingsId == -1)
                throw new InvalidOperationException("Engine settings are not set");

            using (var context = new CarConfiguratorEntityContext())
            {
                //accessories
                var selectedIds = SessionData.ActiveConfiguration.AccessoryIds;
                var accessories = context.Accessories.ToList()
                    .Select(cur => new AccessoryViewModel(cur) { IsSelected = selectedIds?.Contains(cur.Id) ?? false })
                    .OrderBy(cur => cur.Price)
                    .ToList();

                //engine
                var selectedEngineSetting = new EngineSettingsViewModel(context.EngineSettings.First(cur => cur.Id == SessionData.ActiveConfiguration.EngineSettingsId));

                //paint
                PaintViewModel selectedPaint = null;
                if (SessionData.ActiveConfiguration.PaintId != -1)
                    selectedPaint = new PaintViewModel(context.Paints.First(cur => cur.Id == SessionData.ActiveConfiguration.PaintId));

                //rims
                RimViewModel selectedRims = null;
                if (SessionData.ActiveConfiguration.RimId != -1)
                    selectedRims = new RimViewModel(context.Rims.First(cur => cur.Id == SessionData.ActiveConfiguration.RimId));

                return View(new AccessoriesPageViewModel
                {
                    Accessories = accessories,
                    SelectedEngineSetting = selectedEngineSetting,
                    SelectedPaint = selectedPaint,
                    SelectedRims = selectedRims,
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetSelectedAccessories(string data)
        {
            if (!Request.IsAjaxRequest())
                throw new InvalidOperationException("This action must be called with ajax");

            var ids = data.Trim('[', ']').Split(',').Select(cur => int.Parse(cur)).ToList();
            SessionData.ActiveConfiguration.AccessoryIds = ids.ToArray();
            return new EmptyResult();
        }
    }
}
