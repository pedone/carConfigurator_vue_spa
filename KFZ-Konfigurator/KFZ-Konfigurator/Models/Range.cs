using KFZ_Konfigurator.Helper;
using System;

namespace KFZ_Konfigurator.Models
{
    public abstract class Range<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }
    }

    public class DoubleRange : Range<double>
    {
        public double GetRandom(int decimals = 2) => Math.Round(MathHelper.RandomDouble(Min, Max), decimals);
    }

    public class IntRange : Range<int>
    {
        public int GetRandom() => MathHelper.RandomInt(Min, Max);
    }
}