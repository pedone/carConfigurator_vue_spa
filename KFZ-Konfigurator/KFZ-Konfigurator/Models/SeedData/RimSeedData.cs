using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{

    public static class RimSeedData
    {
        public static IEnumerable<Rim> Data
        {
            get
            {
                yield return new Rim { Id = 0, Size = 16, Price = 0 };
                yield return new Rim { Id = 1, Size = 17, Price = 600 };
                yield return new Rim { Id = 2, Size = 18, Price = 1400 };
                yield return new Rim { Id = 3, Size = 19, Price = 2400 };
            }
        }
    }
}