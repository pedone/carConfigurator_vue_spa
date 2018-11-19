using KFZ_Konfigurator.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{

    public abstract class BaseSeedData<T>
    {
        public abstract IEnumerable<T> GetData();
    }

    public static class SeedData
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SeedData));

        public static void Initialize()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                ClearDB(context);

                // let's assume for now that if there's no data in db.carModel, then there's no data yet at all
                if (!context.CarModels.Any())
                {
                    var settingsData = EngineSettingsSeedData.GetRawData();
                    var carModels = CarModelSeedData.Data.ToList();
                    var engines = EngineSeedData.Data.ToList();
                    var engineSettings = EngineSettingsSeedData.Initialize(engines, carModels);

                    Log.Info("adding database seed data");

                    context.CarModels.AddRange(carModels);
                    context.Engines.AddRange(engines);
                    context.EngineSettings.AddRange(engineSettings);
                }

                //if (!context.Rims.Any())
                //{
                //    Log.Info("adding rim seed data");
                //    context.Rims.AddRange(Generate(newRimFunc, 15).Distinct());
                //}

                //if (!context.Paints.Any())
                //{
                //    Log.Info("adding paint seed data");
                //    context.Paints.AddRange(GeneratePaints());
                //}

                //if (!context.Accessories.Any())
                //{
                //    Log.Info("adding accessory seed data");
                //    context.Accessories.AddRange(GenerateAccessories());
                //}

                if (context.ChangeTracker.HasChanges())
                    context.SaveChanges();
            }
        }

        private static void ClearDB(CarConfiguratorEntityContext context)
        {
            context.EngineSettings.RemoveRange(context.EngineSettings);
            context.CarModels.RemoveRange(context.CarModels);
            context.Engines.RemoveRange(context.Engines);
            //context.Accessories.RemoveRange(context.Accessories);
            //context.Paints.RemoveRange(context.Paints);
            //context.Rims.RemoveRange(context.Rims);

            context.SaveChanges();
        }

        //    private static IEnumerable<Accessory> GenerateAccessories()
        //    {
        //        yield return new Accessory { Id = 0, Name = "Parking Assistant", Category = AccessoryCategory.Assistence, Price = 1750 };
        //        yield return new Accessory { Id = 1, Name = "Head-Up Display", Category = AccessoryCategory.Assistence, Price = 980 };
        //        yield return new Accessory { Id = 2, Name = "Rear View Camera", Category = AccessoryCategory.Assistence, Price = 450 };
        //        yield return new Accessory { Id = 3, Name = "Traffic Sign Recognition", Category = AccessoryCategory.Assistence, Price = 300 };
        //        yield return new Accessory { Id = 4, Name = "Sports Chassis", Category = AccessoryCategory.Driving, Price = 500 };
        //        yield return new Accessory { Id = 5, Name = "Comfort Suspension", Category = AccessoryCategory.Driving, Price = 980 };
        //        yield return new Accessory { Id = 6, Name = "Smartphone Interface", Category = AccessoryCategory.Entertainment, Price = 300 };
        //        yield return new Accessory { Id = 7, Name = "Digital Radia", Category = AccessoryCategory.Entertainment, Price = 320 };
        //        yield return new Accessory { Id = 8, Name = "Foldable Rear View Mirror", Category = AccessoryCategory.Exterior, Price = 100 };
        //        yield return new Accessory { Id = 9, Name = "All Weather Floor Mats", Category = AccessoryCategory.Interior, Price = 70 };
        //        yield return new Accessory { Id = 10, Name = "Virtual Cockpit", Category = AccessoryCategory.Navigation, Price = 500 };
        //        yield return new Accessory { Id = 11, Name = "MMI Navigation", Category = AccessoryCategory.Navigation, Price = 1500 };
        //        yield return new Accessory { Id = 12, Name = "Fork Mount Bike Rack", Category = AccessoryCategory.Transport, Price = 190 };
        //    }
    }
}