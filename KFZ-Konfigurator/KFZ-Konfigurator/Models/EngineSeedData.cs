using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models
{
    public static class EngineSeedData
    {
        public static IEnumerable<Engine> Data
        {
            get
            {
                yield return new Engine
                {
                    Id = 0,
                    Gears = 6,
                    Acceleration = 8.9,
                    Emission = 139,
                    Consumption = 6.1,
                    EngineKind = EngineKind.TFSI,
                    Liter = 35,
                    Performance = 110,
                    Price = 34550,
                    EngineSize = 1984,
                    TopSpeed = 219,
                    WheelDrive = WheelDriveKind.Front
                };
                yield return new Engine
                {
                    Id = 1,
                    Gears = 6,
                    Acceleration = 7.5,
                    Emission = 138,
                    Consumption = 6.1,
                    EngineKind = EngineKind.TFSI,
                    Liter = 40,
                    Performance = 140,
                    Price = 38650,
                    EngineSize = 1984,
                    TopSpeed = 236,
                    WheelDrive = WheelDriveKind.Front
                };
                yield return new Engine
                {
                    Id = 2,
                    Gears = 7,
                    Acceleration = 9.2,
                    Emission = 121,
                    Consumption = 4.6,
                    EngineKind = EngineKind.TDI,
                    Liter = 35,
                    Performance = 110,
                    Price = 40950,
                    EngineSize = 1968,
                    TopSpeed = 213,
                    WheelDrive = WheelDriveKind.Front
                };
                yield return new Engine
                {
                    Id = 3,
                    Gears = 7,
                    Acceleration = 7.9,
                    Emission = 121,
                    Consumption = 4.6,
                    EngineKind = EngineKind.TDI,
                    Liter = 40,
                    Performance = 110,
                    Price = 44000,
                    EngineSize = 1968,
                    TopSpeed = 231,
                    WheelDrive = WheelDriveKind.Front
                };
                yield return new Engine
                {
                    Id = 4,
                    Gears = 7,
                    Acceleration = 7.6,
                    Emission = 140,
                    Consumption = 5.3,
                    EngineKind = EngineKind.TDI,
                    Liter = 40,
                    Performance = 140,
                    Price = 46350,
                    EngineSize = 1968,
                    TopSpeed = 230,
                    WheelDrive = WheelDriveKind.All
                };
            }
        }
    }
}