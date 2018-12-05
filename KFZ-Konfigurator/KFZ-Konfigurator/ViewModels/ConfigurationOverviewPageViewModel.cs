using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ConfigurationOverviewPageViewModel : ConfigurationBaseViewModel
    {
        public ConfigurationViewModel Configuration { get; private set; }

        public ConfigurationOverviewPageViewModel(CarConfiguratorEntityContext context)
            : base(context, onlySelectedAccessories: true, onlySelectedEngineSettings: true, onlySelectedPaints: true, onlySelectedRims: true)
        {
            Configuration = new ConfigurationViewModel
            {
                Accessories = Accessories,
                EngineSettings = EngineSettings.FirstOrDefault(),
                Paint = Paints.FirstOrDefault(),
                Rims = Rims.FirstOrDefault()
            };
            Configuration.InitPrice();
        }

    }
}