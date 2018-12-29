using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ExteriorPageViewModel : ConfigurationBaseViewModel
    {
        public IEnumerable<string> PaintCategories { get; set; }

        public ExteriorPageViewModel(CarConfiguratorEntityContext context)
            : base(context, onlySelectedAccessories: true, onlySelectedEngineSettings: true)
        {
        }
    }
}