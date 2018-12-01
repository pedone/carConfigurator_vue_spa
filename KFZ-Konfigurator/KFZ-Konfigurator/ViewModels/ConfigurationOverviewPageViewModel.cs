using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ConfigurationOverviewPageViewModel : ViewModelBase
    {
        public IEnumerable<AccessoryViewModel> Accessories { get; set; }
        public EngineSettingsViewModel EngineSetting { get; set; }
        public PaintViewModel Paint { get; set; }
        public RimViewModel Rims { get; set; }
        public string ConfigurationLink { get; set; }

        public ConfigurationOverviewPageViewModel() { }
        public ConfigurationOverviewPageViewModel(Configuration model, string configurationLink)
        {
            Accessories = model.Accessories.Select(cur => new AccessoryViewModel(cur)).ToList();
            EngineSetting = new EngineSettingsViewModel(model.EngineSetting);
            Paint = new PaintViewModel(model.Paint);
            Rims = new RimViewModel(model.Rims);
            ConfigurationLink = configurationLink;
        }
    }
}