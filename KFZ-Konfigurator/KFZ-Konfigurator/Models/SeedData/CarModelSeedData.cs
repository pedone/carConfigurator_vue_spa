using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Models.SeedData
{

    public enum CarModelId
    {
        A1Sportback2018 = 0,
        A3Sportback2018 = 1,
        A3Sportback2019 = 2,
        A3Limousine2019 = 3,
        A3Cabriolet2019 = 4,
        A3Limousine2018 = 5,
        A3Cabriolet2018 = 6,
        A4Limousine2019 = 7,
        A4Limousine2018 = 8,
        A4Avant2019 = 9,
        A4Avant2018 = 10,
        A5Coupe2018 = 11,
        A5Sportback2019 = 12,
        A5Sportback2018 = 13,
        A5Cabriolet2019 = 14,
        A5Cabriolet2018 = 15,
        A6Limousine2019 = 16,
        A6Avant2019 = 17,
        A7Sportback2019 = 18,
        A8Limousine2019 = 19
    }

    public static class CarModelSeedData
    {
        public static IEnumerable<CarModel> Data
        {
            get
            {
                // A1
                yield return new CarModel { Id = (int)CarModelId.A1Sportback2018, Series = "A1", BodyType = BodyKind.Sportback, Year = 2018 };
                // A3
                yield return new CarModel { Id = (int)CarModelId.A3Sportback2018, Series = "A3", BodyType = BodyKind.Sportback, Year = 2018 };
                yield return new CarModel { Id = (int)CarModelId.A3Sportback2019, Series = "A3", BodyType = BodyKind.Sportback, Year = 2019 };
                yield return new CarModel { Id = (int)CarModelId.A3Limousine2019, Series = "A3", BodyType = BodyKind.Limousine, Year = 2019 };
                yield return new CarModel { Id = (int)CarModelId.A3Cabriolet2019, Series = "A3", BodyType = BodyKind.Cabriolet, Year = 2019 };
                yield return new CarModel { Id = (int)CarModelId.A3Limousine2018, Series = "A3", BodyType = BodyKind.Limousine, Year = 2018 };
                yield return new CarModel { Id = (int)CarModelId.A3Cabriolet2018, Series = "A3", BodyType = BodyKind.Cabriolet, Year = 2018 };
                // A4
                yield return new CarModel { Id = (int)CarModelId.A4Limousine2019, Series = "A4", BodyType = BodyKind.Limousine, Year = 2019 };
                yield return new CarModel { Id = (int)CarModelId.A4Limousine2018, Series = "A4", BodyType = BodyKind.Limousine, Year = 2018 };
                yield return new CarModel { Id = (int)CarModelId.A4Avant2019, Series = "A4", BodyType = BodyKind.Avant, Year = 2019 };
                yield return new CarModel { Id = (int)CarModelId.A4Avant2018, Series = "A4", BodyType = BodyKind.Avant, Year = 2018 };
                // A5
                yield return new CarModel { Id = (int)CarModelId.A5Coupe2018, Series = "A5", BodyType = BodyKind.Coupé, Year = 2018 };
                yield return new CarModel { Id = (int)CarModelId.A5Sportback2019, Series = "A5", BodyType = BodyKind.Sportback, Year = 2019 };
                yield return new CarModel { Id = (int)CarModelId.A5Sportback2018, Series = "A5", BodyType = BodyKind.Sportback, Year = 2018 };
                yield return new CarModel { Id = (int)CarModelId.A5Cabriolet2019, Series = "A5", BodyType = BodyKind.Cabriolet, Year = 2019 };
                yield return new CarModel { Id = (int)CarModelId.A5Cabriolet2018, Series = "A5", BodyType = BodyKind.Cabriolet, Year = 2018 };
                // A6
                yield return new CarModel { Id = (int)CarModelId.A6Limousine2019, Series = "A6", BodyType = BodyKind.Limousine, Year = 2019 };
                yield return new CarModel { Id = (int)CarModelId.A6Avant2019, Series = "A6", BodyType = BodyKind.Avant, Year = 2019 };
                // A7
                yield return new CarModel { Id = (int)CarModelId.A7Sportback2019, Series = "A7", BodyType = BodyKind.Sportback, Year = 2019 };
                // A8
                yield return new CarModel { Id = (int)CarModelId.A8Limousine2019, Series = "A8", BodyType = BodyKind.Limousine, Year = 2019 };
            }
        }
    }
}