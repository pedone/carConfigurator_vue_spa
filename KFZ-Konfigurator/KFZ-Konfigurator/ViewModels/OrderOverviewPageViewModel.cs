using KFZ_Konfigurator.Helper;
using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class OrderOverviewPageViewModel : ItemViewModelBase
    {
        public string Model { get; }
        public double BasePrice { get; }
        public double ExtrasPrice { get; }
        public string Description { get; }
        public DateTime DateTime { get; }
        public bool OrderSuccess { get; }
        public ConfigurationViewModel Configuration { get; }

        public OrderOverviewPageViewModel(Order model, bool orderSuccess = false)
            : base(model.Id, model.BasePrice + model.ExtrasPrice)
        {
            var carModel = model.Configuration.EngineSetting.CarModel;
            Model = $"{carModel.SeriesCategory.Name} {carModel.BodyCategory.Name} {carModel.Year}";

            BasePrice = model.BasePrice;
            ExtrasPrice = model.ExtrasPrice;
            Description = model.Description;
            DateTime = model.DateTime;
            OrderSuccess = orderSuccess;
            Configuration = new ConfigurationViewModel(model.Configuration);
        }

    }
}