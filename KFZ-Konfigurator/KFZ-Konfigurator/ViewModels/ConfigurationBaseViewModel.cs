using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public abstract class ConfigurationBaseViewModel : ViewModelBase
    {
        public IEnumerable<AccessoryViewModel> Accessories { get; private set; }
        public IEnumerable<EngineSettingsViewModel> EngineSettings { get; private set; }
        public IEnumerable<PaintViewModel> Paints { get; private set; }
        public IEnumerable<RimViewModel> Rims { get; private set; }

        protected ConfigurationBaseViewModel(CarConfiguratorEntityContext context, bool onlySelectedAccessories = false,
            bool onlySelectedEngineSettings = false, bool onlySelectedPaints = false, bool onlySelectedRims = false)
        {
            var config = SessionData.ActiveConfiguration;
            var carModel = context.CarModels.First(cur => cur.Id == config.CarModel.Id);

            //accessories
            Accessories = context.Accessories.ToList()
                .Select(cur => new AccessoryViewModel(cur) { IsSelected = config.AccessoryIds.Contains(cur.Id) })
                .OrderBy(cur => cur.Price)
                .ToList();
            if (onlySelectedAccessories)
                Accessories = Accessories.Where(cur => cur.IsSelected).ToList();

            //engine settings
            EngineSettings = carModel.EngineSettings.ToList()
                .Select(cur => new EngineSettingsViewModel(cur) { IsSelected = (cur.Id == config.EngineSettingsId) })
                .OrderBy(cur => cur.Price)
                .ToList();
            if (onlySelectedEngineSettings)
                EngineSettings = EngineSettings.Where(cur => cur.IsSelected).ToList();

            //paints
            Paints = context.Paints.ToList()
                .Select(cur => new PaintViewModel(cur) { IsSelected = (cur.Id == config.PaintId) })
                .OrderBy(cur => cur.Name)
                .ToList();
            if (onlySelectedPaints)
                Paints = Paints.Where(cur => cur.IsSelected).ToList();

            //rims
            Rims = context.Rims.ToList()
                    .Select(cur => new RimViewModel(cur) { IsSelected = (cur.Id == config.RimId) })
                    .ToList();
            if (onlySelectedRims)
                Rims = Rims.Where(cur => cur.IsSelected).ToList();
        }
    }
}