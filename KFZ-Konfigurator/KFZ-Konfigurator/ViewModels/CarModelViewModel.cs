using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class CarModelViewModel : ViewModelBase
    {
        public BodyKind BodyType { get; }
        public string Series { get; }
        public string Model { get; }
        public int Year { get; }
        public EngineSettingsViewModel BaseSettings { get; }

        public CarModelViewModel(CarModel model)
            : base(model.Id)
        {
            BodyType = model.BodyType;
            Series = model.Series;
            Year = model.Year;
            BaseSettings = new EngineSettingsViewModel(GetBaseSettings(model.EngineSettings));
        }

        private EngineSettings GetBaseSettings(IEnumerable<EngineSettings> settings)
        {
            var minPrice = settings.Min(cur => cur.Price);
            return settings.FirstOrDefault(cur => cur.Price == minPrice);
        }
    }
}