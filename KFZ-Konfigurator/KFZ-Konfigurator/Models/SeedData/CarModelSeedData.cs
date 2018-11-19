using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{

    public enum CarModelId
    {
        A1Sportback2018,
        A3Sportback2018,
        A3Sportback2019,
        A3Limousine2019,
        A3Cabriolet2019,
        A3Limousine2018,
    }

    public static class CarModelSeedData
    {
        public static IEnumerable<CarModel> Data
        {
            get
            {
                yield return new CarModel { Id = (int)CarModelId.A1Sportback2018, Series = "A1", BodyType = BodyKind.Sportback, Year = 2018 };
                yield return new CarModel { Id = (int)CarModelId.A3Sportback2018, Series = "A3", BodyType = BodyKind.Sportback, Year = 2018 };
                yield return new CarModel { Id = (int)CarModelId.A3Sportback2019, Series = "A3", BodyType = BodyKind.Sportback, Year = 2019 };
                yield return new CarModel { Id = (int)CarModelId.A3Limousine2019, Series = "A3", BodyType = BodyKind.Limousine, Year = 2018 };
                yield return new CarModel { Id = (int)CarModelId.A3Cabriolet2019, Series = "A3", BodyType = BodyKind.Limousine, Year = 2019 };
                yield return new CarModel { Id = (int)CarModelId.A3Limousine2018, Series = "A3", BodyType = BodyKind.Cabriolet, Year = 2019 };
            }
        }
    }
}