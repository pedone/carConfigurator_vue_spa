using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{

    public static class AccessorySeedData
    {
        public static IEnumerable<Accessory> Data
        {
            get
            {
                yield return new Accessory { Id = 0, Category = AccessoryCategory.Comfort, SubCategory = AccessorySubCategory.Mirror, Name = "Electronic Outside Mirror", Price = 345 };
                yield return new Accessory { Id = 1, Category = AccessoryCategory.Comfort, SubCategory = AccessorySubCategory.Climate, Name = "3 Zone Climate Control", Price = 695 };
                yield return new Accessory { Id = 2, Category = AccessoryCategory.Comfort, SubCategory = AccessorySubCategory.Locksystems, Name = "Anti Theft Warning System", Price = 480 };
                yield return new Accessory { Id = 3, Category = AccessoryCategory.Infotainment, SubCategory = AccessorySubCategory.Navigation, Name = "Virtual Cockpit", Price = 500 };
                yield return new Accessory { Id = 4, Category = AccessoryCategory.Infotainment, SubCategory = AccessorySubCategory.Navigation, Name = "MMI Navigation", Price = 1500 };
                yield return new Accessory { Id = 5, Category = AccessoryCategory.Infotainment, SubCategory = AccessorySubCategory.Entertainment, Name = "Audi Sound System", Price = 290 };
                yield return new Accessory { Id = 6, Category = AccessoryCategory.Infotainment, SubCategory = AccessorySubCategory.Entertainment, Name = "Smartphone Interface", Price = 400 };
                yield return new Accessory { Id = 7, Category = AccessoryCategory.AssistenceSystems, SubCategory = AccessorySubCategory.None, Name = "Parking Assistant", Price = 1750 };
                yield return new Accessory { Id = 8, Category = AccessoryCategory.AssistenceSystems, SubCategory = AccessorySubCategory.None, Name = "Tour Assistentce Package", Price = 1640 };
                yield return new Accessory { Id = 9, Category = AccessoryCategory.AssistenceSystems, SubCategory = AccessorySubCategory.None, Name = "Pre sense basic", Price = 250 };
                yield return new Accessory { Id = 10, Category = AccessoryCategory.AssistenceSystems, SubCategory = AccessorySubCategory.None, Name = "Active lane assist", Price = 600 };
                yield return new Accessory { Id = 11, Category = AccessoryCategory.AssistenceSystems, SubCategory = AccessorySubCategory.None, Name = "Cruise control", Price = 300 };
                yield return new Accessory { Id = 12, Category = AccessoryCategory.AssistenceSystems, SubCategory = AccessorySubCategory.None, Name = "Head-Up Display", Price = 980 };
                yield return new Accessory { Id = 13, Category = AccessoryCategory.AssistenceSystems, SubCategory = AccessorySubCategory.None, Name = "Rear view camera", Price = 450 };
                yield return new Accessory { Id = 14, Category = AccessoryCategory.TechnologyAndSafety, SubCategory = AccessorySubCategory.DrivingAndBrakes, Name = "Comfort Suspension", Price = 980 };
                yield return new Accessory { Id = 15, Category = AccessoryCategory.TechnologyAndSafety, SubCategory = AccessorySubCategory.DrivingAndBrakes, Name = "Sport chassis", Price = 340 };
                yield return new Accessory { Id = 16, Category = AccessoryCategory.TechnologyAndSafety, SubCategory = AccessorySubCategory.None, Name = "Side airbags in the back", Price = 360 };
            }
        }
    }
}