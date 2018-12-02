using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DisplayFormat(DataFormatString = Constants.CurrencyFormat)]
        public double ExtrasPrice { get; private set; }
        [DisplayFormat(DataFormatString = Constants.CurrencyFormat)]
        public double BasePrice { get; private set; }
        [DisplayFormat(DataFormatString = Constants.CurrencyFormat)]
        public double FullPrice
        {
            get => BasePrice + ExtrasPrice;
        }

        public ConfigurationOverviewPageViewModel() { }
        public ConfigurationOverviewPageViewModel(Configuration model)
        {
            Accessories = model.Accessories.Select(cur => new AccessoryViewModel(cur)).ToList();
            EngineSetting = new EngineSettingsViewModel(model.EngineSetting);
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
            BasePrice = EngineSetting.Price;
        }
    }
}