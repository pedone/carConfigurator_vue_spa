using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Helper
{
    public static class MathHelper
    {
        private static Random Random { get; } = new Random();

        public static double RandomDouble(double min, double max) => Random.NextDouble() * (max - min) + min;

        public static float RandomFloat(float min, float max) => (float)RandomDouble(min, max);

        /// <param name="min">including</param>
        /// <param name="max">excluding</param>
        public static int RandomInt(int min, int max) => Random.Next(min, max);

        /// <param name="min">including</param>
        /// <param name="max">including</param>
        public static int RandomIntInclusive(int min, int max) => Random.Next(min, max + 1);

        public static T RandomItem<T>(IEnumerable<T> data) => data.ElementAt(Random.Next(0, data.Count()));

        public static T RandomEnum<T>()
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enum");

            var values = Enum.GetValues(typeof(T));
            return (T)Enum.ToObject(typeof(T), Random.Next(0, values.Length));
        }

    }
}