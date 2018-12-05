using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KFZ_Konfigurator.Models;

namespace KFZ_Konfigurator.ViewModels
{
    public class EngineSettingsPageViewModel : ConfigurationBaseViewModel
    {
        public EngineSettingsPageViewModel(CarConfiguratorEntityContext context)
            : base(context, onlySelectedAccessories: true, onlySelectedPaints: true, onlySelectedRims: true)
        { }
    }
}