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

        [DisplayFormat(DataFormatString = Constants.CurrencyFormat)]
        public double ExtrasPrice { get; private set; }
        [DisplayFormat(DataFormatString = Constants.CurrencyFormat)]
        public double BasePrice { get; private set; }
        [DisplayFormat(DataFormatString = Constants.CurrencyFormat)]
        public double FullPrice
        {
            get => BasePrice + ExtrasPrice;
        }

        public ConfigurationViewModel() { }
        public ConfigurationViewModel(Configuration model)
        {
            Accessories = model.Accessories.Select(cur => new AccessoryViewModel(cur)).ToList();
            EngineSettings = new EngineSettingsViewModel(model.EngineSetting);
            Paint = new PaintViewModel(model.Paint);
            Rims = new RimViewModel(model.Rims);

            InitPrice();
        }

        public void InitPrice()
        {
            var accessoryPrices = Accessories.Select(cur => cur.Price).ToList();
            ExtrasPrice = Paint.Price +
                Rims.Price +
                (accessoryPrices.Count > 0 ? accessoryPrices.Aggregate((result, next) => result + next) : 0);
            BasePrice = EngineSettings.Price;
        }
    }
}