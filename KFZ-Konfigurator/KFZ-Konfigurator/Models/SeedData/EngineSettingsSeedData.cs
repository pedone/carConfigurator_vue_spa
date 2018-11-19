using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{
    public static class EngineSettingsSeedData
    {
        public class EngineSettingsContainer
        {
            public EngineSettings Settings { get; set; }
            public CarModelId CarModelId { get; set; }
            public EngineId EngineId { get; set; }
        }

        private static IEnumerable<EngineSettingsContainer> Data
        {
            get
            {
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 0, Gears = 6, Acceleration = 9.5, Consumption = 4.9, Emission = 111, TopSpeed = 203, Price = 21150 },
                    CarModelId = CarModelId.A1Sportback2018,
                    EngineId = EngineId.TFSI30
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 1, Gears = 7, Acceleration = 9.4, Consumption = 4.9, Emission = 111, TopSpeed = 203, Price = 22850 },
                    CarModelId = CarModelId.A1Sportback2018,
                    EngineId = EngineId.TFSI30
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 2, Gears = 6, Acceleration = 9.9, Consumption = 5.1, Emission = 118, TopSpeed = 206, Price = 25350 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TFSI30
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 3, Gears = 7, Acceleration = 9.9, Consumption = 5.2, Emission = 118, TopSpeed = 206, Price = 27350 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TFSI30
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 4, Gears = 6, Acceleration = 9.9, Consumption = 5.1, Emission = 117, TopSpeed = 211, Price = 28150 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TFSI30
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 5, Gears = 7, Acceleration = 9.9, Consumption = 5.3, Emission = 120, TopSpeed = 211, Price = 30150 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TFSI30
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 6, Gears = 6, Acceleration = 8.2, Consumption = 5.5, Emission = 125, TopSpeed = 220, Price = 27300 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TFSI35
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 7, Gears = 7, Acceleration = 8.2, Consumption = 5.5, Emission = 117, TopSpeed = 220, Price = 29300 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TFSI35
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 8, Gears = 6, Acceleration = 8.2, Consumption = 5.5, Emission = 124, TopSpeed = 224, Price = 30100 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TFSI35
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 9, Gears = 7, Acceleration = 8.2, Consumption = 5.1, Emission = 117, TopSpeed = 224, Price = 32100 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TFSI35
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 10, Gears = 6, Acceleration = 8.9, Consumption = 5.7, Emission = 129, TopSpeed = 222, Price = 34100 },
                    CarModelId = CarModelId.A3Cabriolet2019,
                    EngineId = EngineId.TFSI35
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 11, Gears = 7, Acceleration = 8.9, Consumption = 5.3, Emission = 121, TopSpeed = 222, Price = 36100 },
                    CarModelId = CarModelId.A3Cabriolet2019,
                    EngineId = EngineId.TFSI35
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 12, Gears = 6, Acceleration = 10.4, Consumption = 4.5, Emission = 118, TopSpeed = 205, Price = 28350 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TDI30
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 13, Gears = 7, Acceleration = 10.4, Consumption = 4, Emission = 105, TopSpeed = 205, Price = 30350 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TDI30
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 14, Gears = 6, Acceleration = 10.4, Consumption = 4.4, Emission = 117, TopSpeed = 211, Price = 31150 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TDI30
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 15, Gears = 7, Acceleration = 10.4, Consumption = 3.9, Emission = 104, TopSpeed = 211, Price = 33150 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TDI30
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 16, Gears = 7, Acceleration = 8.1, Consumption = 4.6, Emission = 119, TopSpeed = 218, Price = 32550 },
                    CarModelId = CarModelId.A3Sportback2018,
                    EngineId = EngineId.TDI20
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 17, Gears = 7, Acceleration = 8.1, Consumption = 4.6, Emission = 118, TopSpeed = 224, Price = 33450 },
                    CarModelId = CarModelId.A3Limousine2018,
                    EngineId = EngineId.TDI20
                };
            }
        }

        public static IEnumerable<EngineSettingsContainer> GetRawData()
        {
            return Data;
        }

        public static IEnumerable<EngineSettings> Initialize(IEnumerable<Engine> engines, IEnumerable<CarModel> carModels)
        {
            var result = Data.ToList();
            foreach (var curSetting in result)
            {
                curSetting.Settings.Engine = engines.FirstOrDefault(cur => cur.Id == (int)curSetting.EngineId) ?? throw new ArgumentNullException($"engine with id {(int)curSetting.EngineId} not found");
                curSetting.Settings.CarModel = carModels.FirstOrDefault(cur => cur.Id == (int)curSetting.CarModelId) ?? throw new ArgumentNullException($"car model with id {(int)curSetting.CarModelId} not found");
            }

            return result.Select(cur => cur.Settings);
        }
    }
}
