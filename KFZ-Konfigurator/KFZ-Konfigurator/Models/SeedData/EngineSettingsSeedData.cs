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
                    EngineId = EngineId.TFSI30_999_85
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 1, Gears = 7, Acceleration = 9.4, Consumption = 4.9, Emission = 111, TopSpeed = 203, Price = 22850 },
                    CarModelId = CarModelId.A1Sportback2018,
                    EngineId = EngineId.TFSI30_999_85
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 2, Gears = 6, Acceleration = 9.9, Consumption = 5.1, Emission = 118, TopSpeed = 206, Price = 25350 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TFSI30_999_85
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 3, Gears = 7, Acceleration = 9.9, Consumption = 5.2, Emission = 118, TopSpeed = 206, Price = 27350 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TFSI30_999_85
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 4, Gears = 6, Acceleration = 9.9, Consumption = 5.1, Emission = 117, TopSpeed = 211, Price = 28150 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TFSI30_999_85
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 5, Gears = 7, Acceleration = 9.9, Consumption = 5.3, Emission = 120, TopSpeed = 211, Price = 30150 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TFSI30_999_85
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 6, Gears = 6, Acceleration = 8.2, Consumption = 5.5, Emission = 125, TopSpeed = 220, Price = 27300 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 7, Gears = 7, Acceleration = 8.2, Consumption = 5.5, Emission = 117, TopSpeed = 220, Price = 29300 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 8, Gears = 6, Acceleration = 8.2, Consumption = 5.5, Emission = 124, TopSpeed = 224, Price = 30100 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 9, Gears = 7, Acceleration = 8.2, Consumption = 5.1, Emission = 117, TopSpeed = 224, Price = 32100 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 10, Gears = 6, Acceleration = 8.9, Consumption = 5.7, Emission = 129, TopSpeed = 222, Price = 34100 },
                    CarModelId = CarModelId.A3Cabriolet2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 11, Gears = 7, Acceleration = 8.9, Consumption = 5.3, Emission = 121, TopSpeed = 222, Price = 36100 },
                    CarModelId = CarModelId.A3Cabriolet2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 12, Gears = 6, Acceleration = 10.4, Consumption = 4.5, Emission = 118, TopSpeed = 205, Price = 28350 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TDI30_2967_160
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 13, Gears = 7, Acceleration = 10.4, Consumption = 4, Emission = 105, TopSpeed = 205, Price = 30350 },
                    CarModelId = CarModelId.A3Sportback2019,
                    EngineId = EngineId.TDI30_2967_160
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 14, Gears = 6, Acceleration = 10.4, Consumption = 4.4, Emission = 117, TopSpeed = 211, Price = 31150 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TDI30_2967_160
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 15, Gears = 7, Acceleration = 10.4, Consumption = 3.9, Emission = 104, TopSpeed = 211, Price = 33150 },
                    CarModelId = CarModelId.A3Limousine2019,
                    EngineId = EngineId.TDI30_2967_160
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 16, Gears = 7, Acceleration = 8.1, Consumption = 4.6, Emission = 119, TopSpeed = 218, Price = 32550 },
                    CarModelId = CarModelId.A3Sportback2018,
                    EngineId = EngineId.TDI20_1968_90
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 17, Gears = 7, Acceleration = 8.1, Consumption = 4.6, Emission = 118, TopSpeed = 224, Price = 33450 },
                    CarModelId = CarModelId.A3Limousine2018,
                    EngineId = EngineId.TDI20_1968_90
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 18, Gears = 7, Acceleration = 6.9, Consumption = 6.3, Emission = 144, TopSpeed = 242, Price = 42650 },
                    CarModelId = CarModelId.A3Cabriolet2018,
                    EngineId = EngineId.TFSI20_1984_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 19, Gears = 6, Acceleration = 8.6, Consumption = 6.0, Emission = 137, TopSpeed = 224, Price = 32700 },
                    CarModelId = CarModelId.A4Limousine2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 20, Gears = 7, Acceleration = 8.9, Consumption = 5.9, Emission = 135, TopSpeed = 225, Price = 35000 },
                    CarModelId = CarModelId.A4Limousine2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 21, Gears = 6, Acceleration = 7.2, Consumption = 6.0, Emission = 137, TopSpeed = 241, Price = 36800 },
                    CarModelId = CarModelId.A4Limousine2019,
                    EngineId = EngineId.TFSI40_1984_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 22, Gears = 7, Acceleration = 8.9, Consumption = 4.6, Emission = 120, TopSpeed = 219, Price = 39100 },
                    CarModelId = CarModelId.A4Limousine2019,
                    EngineId = EngineId.TDI35_1968_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 23, Gears = 7, Acceleration = 7.2, Consumption = 5.9, Emission = 137, TopSpeed = 241, Price = 39100 },
                    CarModelId = CarModelId.A4Limousine2019,
                    EngineId = EngineId.TFSI40_1984_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 24, Gears = 7, Acceleration = 7.7, Consumption = 4.5, Emission = 120, TopSpeed = 241, Price = 42150 },
                    CarModelId = CarModelId.A4Limousine2019,
                    EngineId = EngineId.TDI40_1968_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 25, Gears = 7, Acceleration = 7.4, Consumption = 5.2, Emission = 138, TopSpeed = 235, Price = 44500, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A4Limousine2019,
                    EngineId = EngineId.TDI40_1968_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 26, Gears = 7, Acceleration = 10.2, Consumption = 4.3, Emission = 112, TopSpeed = 204, Price = 37700 },
                    CarModelId = CarModelId.A4Limousine2018,
                    EngineId = EngineId.TDI20_1968_90
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 27, Gears = 7, Acceleration = 6.3, Consumption = 4.8, Emission = 127, TopSpeed = 250, Price = 47200, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A4Limousine2018,
                    EngineId = EngineId.TDI30_2967_160
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 28, Gears = 6, Acceleration = 8.9, Consumption = 6.1, Emission = 139, TopSpeed = 219, Price = 34550 },
                    CarModelId = CarModelId.A4Avant2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 29, Gears = 7, Acceleration = 9.2, Consumption = 6.0, Emission = 137, TopSpeed = 220, Price = 36850 },
                    CarModelId = CarModelId.A4Avant2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 30, Gears = 6, Acceleration = 7.5, Consumption = 6.1, Emission = 138, TopSpeed = 236, Price = 38650 },
                    CarModelId = CarModelId.A4Avant2019,
                    EngineId = EngineId.TFSI40_1984_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 31, Gears = 7, Acceleration = 9.2, Consumption = 4.6, Emission = 121, TopSpeed = 213, Price = 40950 },
                    CarModelId = CarModelId.A4Avant2019,
                    EngineId = EngineId.TDI35_1968_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 32, Gears = 7, Acceleration = 7.5, Consumption = 6.0, Emission = 137, TopSpeed = 236, Price = 40950 },
                    CarModelId = CarModelId.A4Avant2019,
                    EngineId = EngineId.TFSI40_1984_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 33, Gears = 7, Acceleration = 7.9, Consumption = 4.6, Emission = 121, TopSpeed = 231, Price = 44000 },
                    CarModelId = CarModelId.A4Avant2019,
                    EngineId = EngineId.TDI40_1968_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 34, Gears = 7, Acceleration = 7.6, Consumption = 5.3, Emission = 140, TopSpeed = 230, Price = 46350, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A4Avant2019,
                    EngineId = EngineId.TDI40_1968_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 35, Gears = 7, Acceleration = 10.6, Consumption = 4.4, Emission = 115, TopSpeed = 198, Price = 39550 },
                    CarModelId = CarModelId.A4Avant2018,
                    EngineId = EngineId.TDI20_1968_90
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 36, Gears = 7, Acceleration = 6.4, Consumption = 4.9, Emission = 129, TopSpeed = 245, Price = 49050, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A4Avant2018,
                    EngineId = EngineId.TDI30_2967_160
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 37, Gears = 7, Acceleration = 6.2, Consumption = 4.8, Emission = 127, TopSpeed = 250, Price = 49900, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A5Coupe2018,
                    EngineId = EngineId.TDI30_2967_160
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 38, Gears = 8, Acceleration = 5.2, Consumption = 5.5, Emission = 143, TopSpeed = 250, Price = 54550, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A5Coupe2018,
                    EngineId = EngineId.TDI30_2967_210
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 39, Gears = 7, Acceleration = 9.1, Consumption = 5.9, Emission = 135, TopSpeed = 223, Price = 38200, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A5Sportback2019,
                    EngineId = EngineId.TFSI35_1984_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 40, Gears = 6, Acceleration = 7.5, Consumption = 6.0, Emission = 137, TopSpeed = 241, Price = 38600, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A5Sportback2019,
                    EngineId = EngineId.TFSI40_1984_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 41, Gears = 7, Acceleration = 7.5, Consumption = 5.9, Emission = 137, TopSpeed = 241, Price = 40900, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A5Sportback2019,
                    EngineId = EngineId.TFSI40_1984_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 42, Gears = 7, Acceleration = 9.1, Consumption = 4.6, Emission = 119, TopSpeed = 217, Price = 41400, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A5Sportback2019,
                    EngineId = EngineId.TDI35_1968_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 43, Gears = 7, Acceleration = 7.9, Consumption = 4.5, Emission = 119, TopSpeed = 241, Price = 43950, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A5Sportback2019,
                    EngineId = EngineId.TDI40_1968_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 44, Gears = 7, Acceleration = 7.6, Consumption = 5.2, Emission = 138, TopSpeed = 235, Price = 46300, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A5Sportback2019,
                    EngineId = EngineId.TDI40_1968_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 45, Gears = 7, Acceleration = 6.4, Consumption = 4.8, Emission = 126, TopSpeed = 250, Price = 49900, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A5Sportback2018,
                    EngineId = EngineId.TDI30_2967_160
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 46, Gears = 8, Acceleration = 5.3, Consumption = 5.7, Emission = 148, TopSpeed = 250, Price = 54550, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A5Sportback2018,
                    EngineId = EngineId.TDI30_2967_210
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 47, Gears = 6, Acceleration = 7.9, Consumption = 6.1, Emission = 140, TopSpeed = 241, Price = 46500, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A5Cabriolet2019,
                    EngineId = EngineId.TFSI40_1984_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 48, Gears = 7, Acceleration = 7.9, Consumption = 6.1, Emission = 140, TopSpeed = 241, Price = 48800, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A5Cabriolet2019,
                    EngineId = EngineId.TFSI40_1984_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 49, Gears = 7, Acceleration = 9.8, Consumption = 4.8, Emission = 128, TopSpeed = 213, Price = 49300, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A5Cabriolet2019,
                    EngineId = EngineId.TDI35_1968_110
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 50, Gears = 7, Acceleration = 8.4, Consumption = 4.9, Emission = 128, TopSpeed = 241, Price = 51850, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A5Cabriolet2019,
                    EngineId = EngineId.TDI40_1968_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 51, Gears = 7, Acceleration = 8, Consumption = 5.1, Emission = 143, TopSpeed = 233, Price = 54200, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A5Cabriolet2019,
                    EngineId = EngineId.TDI40_1968_140
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 52, Gears = 8, Acceleration = 5.7, Consumption = 5.9, Emission = 154, TopSpeed = 250, Price = 60750, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A5Cabriolet2018,
                    EngineId = EngineId.TDI30_2967_210
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 53, Gears = 7, Acceleration = 8.1, Consumption = 4.7, Emission = 124, TopSpeed = 246, Price = 49150, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A6Limousine2019,
                    EngineId = EngineId.TDI40_1968_150
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 54, Gears = 8, Acceleration = 6.3, Consumption = 5.8, Emission = 150, TopSpeed = 250, Price = 55050, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A6Limousine2019,
                    EngineId = EngineId.TDI45_2967_170
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 55, Gears = 8, Acceleration = 5.5, Consumption = 5.8, Emission = 150, TopSpeed = 250, Price = 58050, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A6Limousine2019,
                    EngineId = EngineId.TDI50_2967_210
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 56, Gears = 7, Acceleration = 8.3, Consumption = 4.9, Emission = 129, TopSpeed = 241, Price = 53950, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A6Avant2019,
                    EngineId = EngineId.TDI40_1968_150
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 57, Gears = 8, Acceleration = 6.5, Consumption = 5.9, Emission = 155, TopSpeed = 250, Price = 59850, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A6Avant2019,
                    EngineId = EngineId.TDI45_2967_170
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 58, Gears = 8, Acceleration = 5.7, Consumption = 5.9, Emission = 155, TopSpeed = 250, Price = 62850, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A6Avant2019,
                    EngineId = EngineId.TDI50_2967_210
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 59, Gears = 7, Acceleration = 8.3, Consumption = 4.9, Emission = 127, TopSpeed = 245, Price = 57500, WheelDrive = WheelDriveKind.Front },
                    CarModelId = CarModelId.A7Sportback2019,
                    EngineId = EngineId.TDI40_1968_150
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 60, Gears = 8, Acceleration = 6.5, Consumption = 5.8, Emission = 150, TopSpeed = 250, Price = 62900, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A7Sportback2019,
                    EngineId = EngineId.TDI45_2967_170
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 61, Gears = 8, Acceleration = 5.7, Consumption = 5.8, Emission = 150, TopSpeed = 250, Price = 66300, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A7Sportback2019,
                    EngineId = EngineId.TDI50_2967_210
                };
                yield return new EngineSettingsContainer
                {
                    Settings = new EngineSettings { Id = 62, Gears = 8, Acceleration = 5.9, Consumption = 5.9, Emission = 157, TopSpeed = 250, Price = 91900, WheelDrive = WheelDriveKind.All },
                    CarModelId = CarModelId.A8Limousine2019,
                    EngineId = EngineId.TDI50_2967_210
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
