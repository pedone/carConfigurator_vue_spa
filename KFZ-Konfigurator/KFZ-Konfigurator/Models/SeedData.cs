using KFZ_Konfigurator.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models
{
    public static class SeedData
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SeedData));

        private static class CarData
        {
            public static IntRange BasePriceRange { get; } = new IntRange { Min = 17500, Max = 42300 };
            public static string[] ModelNames { get; } = new string[] { "A1", "A3", "A4", "A5", "A6", "A8", "1er", "2er", "3er", "5er", "7er", "A", "B", "C", "E", "S", "GTI", "Touring", "Gran Tourismo" };
            public static IntRange YearRange { get; } = new IntRange { Min = 1990, Max = 2018 };
        }

        private static class PaintData
        {
            public static IntRange PriceRange { get; } = new IntRange { Min = 100, Max = 900 };
            public static string[] Colors { get; } = new string[] { "Blue", "Silver", "White", "Black", "Gray", "Red", "Green" };
        }

        private static class EngineData
        {
            public static DoubleRange AccelerationRange { get; } = new DoubleRange { Min = 4, Max = 15 };
            public static DoubleRange ConsumptionRange { get; } = new DoubleRange { Min = 4, Max = 13 };
            public static IntRange EmissionRange { get; } = new IntRange { Min = 80, Max = 250 };
            public static IntRange EngineSizeRange { get; } = new IntRange { Min = 1200, Max = 2500 };
            public static IntRange PerformanceRange { get; } = new IntRange { Min = 70, Max = 250 };
            public static IntRange PriceRange { get; } = new IntRange { Min = 2000, Max = 5000 };
            public static IntRange TopSpeedRange { get; } = new IntRange { Min = 120, Max = 200 };
            public static float[] LiterOptions { get; } = new float[] { 2, 2.5f, 3, 3.5f, 4, 4.5f, 5 };
        }

        private static class RimData
        {
            public static IntRange PriceRange { get; } = new IntRange { Min = 500, Max = 5000 };
            public static IntRange SizeRange { get; } = new IntRange { Min = 16, Max = 22 };
            public static string[] Colors { get; } = PaintData.Colors;
        }

        private static readonly Func<int, Car> newCarFunc = (int id) => new Car
        {
            Id = id,
            BasePrice = CarData.BasePriceRange.GetRandom(),
            Model = MathHelper.RandomItem(CarData.ModelNames),
            Year = CarData.YearRange.GetRandom(),
            BodyType = MathHelper.RandomEnum<BodyKind>(),
            Brand = MathHelper.RandomEnum<CarBrandKind>(),
            WheelDrive = MathHelper.RandomEnum<WheelDriveKind>()
        };

        private static readonly Func<int, Engine> newEngineFunc = (int id) => new Engine
        {
            Id = id,
            Acceleration = EngineData.AccelerationRange.GetRandom(1),
            Consumption = EngineData.ConsumptionRange.GetRandom(1),
            Emission = EngineData.EmissionRange.GetRandom(),
            EngineSize = EngineData.EngineSizeRange.GetRandom(),
            Performance = EngineData.PerformanceRange.GetRandom(),
            Price = EngineData.PriceRange.GetRandom(),
            TopSpeed = EngineData.TopSpeedRange.GetRandom(),
            FuelKind = MathHelper.RandomEnum<FuelKind>(),
            Liter = MathHelper.RandomItem(EngineData.LiterOptions)
        };

        private static readonly Func<int, Rim> newRimFunc = (int id) => new Rim
        {
            Id = id,
            Price = RimData.PriceRange.GetRandom(),
            Size = RimData.SizeRange.GetRandom(),
            Color = MathHelper.RandomItem(RimData.Colors),
            Brand = MathHelper.RandomEnum<RimBrandKind>()
        };

        public static void Initialize()
        {
            using (var context = new CarConfiguratorEntityContext())
            {
                if (!context.Cars.Any())
                {
                    Log.Info("adding car seed data");
                    context.Cars.AddRange(Generate(newCarFunc, 10).Distinct());
                }

                if (!context.Engines.Any())
                {
                    Log.Info("adding engine seed data");
                    context.Engines.AddRange(Generate(newEngineFunc, 15).Distinct());
                }

                if (!context.Rims.Any())
                {
                    Log.Info("adding rim seed data");
                    context.Rims.AddRange(Generate(newRimFunc, 15).Distinct());
                }

                if (!context.Paints.Any())
                {
                    Log.Info("adding paint seed data");
                    context.Paints.AddRange(GeneratePaints());
                }

                if (!context.Accessories.Any())
                {
                    Log.Info("adding accessory seed data");
                    context.Accessories.AddRange(GenerateAccessories());
                }

                if (context.ChangeTracker.HasChanges())
                    context.SaveChanges();
            }
        }

        private static IEnumerable<T> Generate<T>(Func<int, T> newItemFunc, int count)
        {
            for (int i = 0; i < count; i++)
                yield return newItemFunc(i);
        }

        private static IEnumerable<Paint> GeneratePaints()
        {
            for (int i = 0; i < PaintData.Colors.Length; i++)
                yield return new Paint
                {
                    Id = i,
                    Color = PaintData.Colors[i],
                    Price = PaintData.PriceRange.GetRandom()
                };
        }

        private static IEnumerable<Accessory> GenerateAccessories()
        {
            yield return new Accessory { Id = 0, Name = "Parking Assistant", Category = AccessoryCategory.Assistence, Price = 1750 };
            yield return new Accessory { Id = 1, Name = "Head-Up Display", Category = AccessoryCategory.Assistence, Price = 980 };
            yield return new Accessory { Id = 2, Name = "Rear View Camera", Category = AccessoryCategory.Assistence, Price = 450 };
            yield return new Accessory { Id = 3, Name = "Traffic Sign Recognition", Category = AccessoryCategory.Assistence, Price = 300 };
            yield return new Accessory { Id = 4, Name = "Sports Chassis", Category = AccessoryCategory.Driving, Price = 500 };
            yield return new Accessory { Id = 5, Name = "Comfort Suspension", Category = AccessoryCategory.Driving, Price = 980 };
            yield return new Accessory { Id = 6, Name = "Smartphone Interface", Category = AccessoryCategory.Entertainment, Price = 300 };
            yield return new Accessory { Id = 7, Name = "Digital Radia", Category = AccessoryCategory.Entertainment, Price = 320 };
            yield return new Accessory { Id = 8, Name = "Foldable Rear View Mirror", Category = AccessoryCategory.Exterior, Price = 100 };
            yield return new Accessory { Id = 9, Name = "All Weather Floor Mats", Category = AccessoryCategory.Interior, Price = 70 };
            yield return new Accessory { Id = 10, Name = "Virtual Cockpit", Category = AccessoryCategory.Navigation, Price = 500 };
            yield return new Accessory { Id = 11, Name = "MMI Navigation", Category = AccessoryCategory.Navigation, Price = 1500 };
            yield return new Accessory { Id = 12, Name = "Fork Mount Bike Rack", Category = AccessoryCategory.Transport, Price = 190 };
        }
    }
}