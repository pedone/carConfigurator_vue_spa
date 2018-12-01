using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.Resources.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class OrderListPageViewModel : ViewModelBase
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}