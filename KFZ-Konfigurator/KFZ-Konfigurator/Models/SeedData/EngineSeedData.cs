using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{
    public enum EngineId
    {
        TFSI30_999_85 = 0,
        TFSI35_1498_110 = 1,
        TDI30_1598_85 = 2,
        TDI20_1968_110 = 3,
        TFSI20_1984_140 = 4,
        TFSI35_1984_110 = 5,
        TFSI40_1984_140 = 6,
        TDI35_1968_110 = 7,
        TDI40_1968_140 = 8,
        TDI20_1968_90 = 9,
        TDI30_2967_160 = 10,
        TDI30_2967_210 = 11,
        TDI40_1968_150 = 12,
        TDI45_2967_170 = 13,
        TDI50_2967_210 = 14
    };

    public static class EngineSeedData
    {
        public static IEnumerable<Engine> Data
        {
            get
            {
                yield return new Engine { Id = (int)EngineId.TFSI30_999_85, EngineKind = EngineKind.TFSI, Liter = 30, Size = 999, Performance = 85 };
                yield return new Engine { Id = (int)EngineId.TFSI35_1498_110, EngineKind = EngineKind.TFSI, Liter = 35, Size = 1498, Performance = 110 };
                yield return new Engine { Id = (int)EngineId.TDI30_1598_85, EngineKind = EngineKind.TDI, Liter = 30, Size = 1598, Performance = 85 };
                yield return new Engine { Id = (int)EngineId.TDI20_1968_110, EngineKind = EngineKind.TDI, Liter = 20, Size = 1968, Performance = 110 };
                yield return new Engine { Id = (int)EngineId.TFSI20_1984_140, EngineKind = EngineKind.TFSI, Liter = 20, Size = 1984, Performance = 140 };
                yield return new Engine { Id = (int)EngineId.TFSI35_1984_110, EngineKind = EngineKind.TFSI, Liter = 35, Size = 1984, Performance = 110 };
                yield return new Engine { Id = (int)EngineId.TFSI40_1984_140, EngineKind = EngineKind.TFSI, Liter = 40, Size = 1984, Performance = 140 };
                yield return new Engine { Id = (int)EngineId.TDI35_1968_110, EngineKind = EngineKind.TDI, Liter = 35, Size = 1968, Performance = 110 };
                yield return new Engine { Id = (int)EngineId.TDI40_1968_140, EngineKind = EngineKind.TDI, Liter = 40, Size = 1968, Performance = 140 };
                yield return new Engine { Id = (int)EngineId.TDI20_1968_90, EngineKind = EngineKind.TDI, Liter = 20, Size = 1968, Performance = 90 };
                yield return new Engine { Id = (int)EngineId.TDI30_2967_160, EngineKind = EngineKind.TDI, Liter = 30, Size = 2967, Performance = 160 };
                yield return new Engine { Id = (int)EngineId.TDI30_2967_210, EngineKind = EngineKind.TDI, Liter = 30, Size = 2967, Performance = 210 };
                yield return new Engine { Id = (int)EngineId.TDI40_1968_150, EngineKind = EngineKind.TDI, Liter = 40, Size = 1968, Performance = 150 };
                yield return new Engine { Id = (int)EngineId.TDI45_2967_170, EngineKind = EngineKind.TDI, Liter = 45, Size = 2967, Performance = 170 };
                yield return new Engine { Id = (int)EngineId.TDI50_2967_210, EngineKind = EngineKind.TDI, Liter = 50, Size = 2967, Performance = 210 };
            }
        }
    }
}
