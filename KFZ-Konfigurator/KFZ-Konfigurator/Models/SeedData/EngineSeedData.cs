using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{
    public enum EngineId
    {
        TFSI30,
        TFSI35,
        TDI30,
        TDI20,
        TFSI20,
    };

    public static class EngineSeedData
    {
        public static IEnumerable<Engine> Data
        {
            get
            {
                yield return new Engine { Id = (int)EngineId.TFSI30, EngineKind = EngineKind.TFSI, Liter = 30, Size = 999, Performance = 85 };
                yield return new Engine { Id = (int)EngineId.TFSI35, EngineKind = EngineKind.TFSI, Liter = 35, Size = 1498, Performance = 110 };
                yield return new Engine { Id = (int)EngineId.TDI30, EngineKind = EngineKind.TDI, Liter = 30, Size = 1598, Performance = 85 };
                yield return new Engine { Id = (int)EngineId.TDI20, EngineKind = EngineKind.TDI, Liter = 20, Size = 1968, Performance = 110 };
                yield return new Engine { Id = (int)EngineId.TFSI20, EngineKind = EngineKind.TFSI, Liter = 20, Size = 1984, Performance = 140 };
            }
        }
    }
}
