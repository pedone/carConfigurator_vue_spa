using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ConfigurationOverviewPageViewModel
    {
        public IEnumerable<AccessoryViewModel> Accessories { get; set; }
        public EngineSettingsViewModel EngineSetting { get; set; }
        public PaintViewModel Paint { get; set; }
        public RimViewModel Rims { get; set; }
        public string ConfigurationLink { get; set; }
    }
}