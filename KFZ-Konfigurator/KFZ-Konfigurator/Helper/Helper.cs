using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.Helper
{
    public static class Helper
    {
        public static string If(bool conditional, string data)
        {
            return conditional ? data : null;
        }

        public static FuelKind EngineKindToFuelKind(EngineKind engineKind)
        {
            switch (engineKind)
            {
                case EngineKind.TDI:
                    return FuelKind.Diesel;
                case EngineKind.TFSI:
                    return FuelKind.Petrol;
                default:
                    throw new NotImplementedException($"unknown engine kind {engineKind}");
            }
        }
    }
}