using KFZ_Konfigurator.Helper;
using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.Resources.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class OrderOverviewPageViewModel : ItemViewModelBase
    {
        public double BasePrice { get; }
        public double ExtrasPrice { get; }
        public string Description { get; }
        public ConfigurationOverviewPageViewModel Configuration { get; }

        public OrderOverviewPageViewModel(Order model, string configurationLink)
            : base(model.Id, model.BasePrice + model.ExtrasPrice)
        {
            BasePrice = model.BasePrice;
            ExtrasPrice = model.ExtrasPrice;
            Description = model.Description;
            Configuration = new ConfigurationOverviewPageViewModel(model.Configuration, configurationLink);
        }

    }
}