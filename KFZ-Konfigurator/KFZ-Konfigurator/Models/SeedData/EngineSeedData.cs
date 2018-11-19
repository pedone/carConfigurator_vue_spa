using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{
    public enum EngineId
    {
        TFSI30,
        TFSI35_1498_110,
        TDI30_1598_85,
        TDI20_1968_110,
        TFSI20,

        TFSI35_1984_110,
        TFSI40,
        TDI35,
        TDI40,
        TDI20_1968_90,
        TDI30_2967_160
    };

    public static class EngineSeedData
    {
        public static IEnumerable<Engine> Data
        {
            get
            {
                yield return new Engine { Id = (int)EngineId.TFSI30, EngineKind = EngineKind.TFSI, Liter = 30, Size = 999, Performance = 85 };
                yield return new Engine { Id = (int)EngineId.TFSI35_1498_110, EngineKind = EngineKind.TFSI, Liter = 35, Size = 1498, Performance = 110 };
                yield return new Engine { Id = (int)EngineId.TDI30_1598_85, EngineKind = EngineKind.TDI, Liter = 30, Size = 1598, Performance = 85 };
                yield return new Engine { Id = (int)EngineId.TDI20_1968_110, EngineKind = EngineKind.TDI, Liter = 20, Size = 1968, Performance = 110 };
                yield return new Engine { Id = (int)EngineId.TFSI20, EngineKind = EngineKind.TFSI, Liter = 20, Size = 1984, Performance = 140 };
                yield return new Engine { Id = (int)EngineId.TFSI35_1984_110, EngineKind = EngineKind.TFSI, Liter = 35, Size = 1984, Performance = 110 };
                yield return new Engine { Id = (int)EngineId.TFSI40, EngineKind = EngineKind.TFSI, Liter = 40, Size = 1984, Performance = 140 };
                yield return new Engine { Id = (int)EngineId.TDI35, EngineKind = EngineKind.TDI, Liter = 35, Size = 1968, Performance = 110 };
                yield return new Engine { Id = (int)EngineId.TDI40, EngineKind = EngineKind.TDI, Liter = 40, Size = 1968, Performance = 140 };
                yield return new Engine { Id = (int)EngineId.TDI20_1968_90, EngineKind = EngineKind.TDI, Liter = 20, Size = 1968, Performance = 90 };
                yield return new Engine { Id = (int)EngineId.TDI30_2967_160, EngineKind = EngineKind.TDI, Liter = 30, Size = 2967, Performance = 160 };
            }
        }
    }
}
