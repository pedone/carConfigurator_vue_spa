using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class AccessoryViewModel
    {
        public int Id { get; }

        public AccessoryViewModel(Accessory model)
        {
            Id = model.Id;
        }
    }
}