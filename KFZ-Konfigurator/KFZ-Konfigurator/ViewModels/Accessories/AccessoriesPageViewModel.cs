using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class AccessoriesPageViewModel : ConfigurationBaseViewModel
    {
        public IEnumerable<string> AccessoryCategories { get; set; }

        public AccessoriesPageViewModel(CarConfiguratorEntityContext context)
            : base(context, onlySelectedEngineSettings: true, onlySelectedPaints: true, onlySelectedRims: true)
        {
        }
    }
}