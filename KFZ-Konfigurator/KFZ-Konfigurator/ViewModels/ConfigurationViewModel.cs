using KFZ_Konfigurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KFZ_Konfigurator.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        public IEnumerable<AccessoryViewModel> Accessories { get; set; }
        public EngineSettingsViewModel EngineSettings { get; set; }
        public PaintViewModel Paint { get; set; }
        public RimViewModel Rims { get; set; }

        public ConfigurationViewModel() { }
        public ConfigurationViewModel(Configuration model)
        {
            Accessories = model.Accessories.Select(cur => new AccessoryViewModel(cur)).ToList();
            EngineSettings = new EngineSettingsViewModel(model.EngineSetting);
            Paint = new PaintViewModel(model.Paint);
            Rims = new RimViewModel(model.Rims);
        }
    }
}