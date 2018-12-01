using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class EngineSettingsPageViewModel : ViewModelBase
    {
        public IEnumerable<EngineSettingsViewModel> EngineSettings { get; set; }
        public IEnumerable<AccessoryViewModel> SelectedAccessories { get; set; }
        public PaintViewModel SelectedPaint { get; set; }
        public RimViewModel SelectedRims { get; set; }
    }
}