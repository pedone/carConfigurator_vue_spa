using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class AccessoryViewModel : ViewModelBase
    {
        public AccessoryCategory Category { get; }
        public string Name { get; }
        public AccessorySubCategory SubCategory { get; }

        public AccessoryViewModel(Accessory model)
            : base(model.Id, model.Price)
        {
            Category = model.Category;
            Name = model.Name;
            SubCategory = model.SubCategory;
        }
    }
}