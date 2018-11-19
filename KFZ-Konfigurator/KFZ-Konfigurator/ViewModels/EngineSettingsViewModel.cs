using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class EngineSettingsViewModel
    {
        public int Id { get; }
        public double Acceleration { get; }
        public double Consumption { get; }
        public int Emission { get; }
        public int Gears { get; }
        public double Price { get; }
        public int TopSpeed { get; }
        public WheelDriveKind WheelDrive { get; }
        public Engine Engine { get; }

        public EngineSettingsViewModel(EngineSettings model)
        {
            Id = model.Id;
            Acceleration = model.Acceleration;
            Consumption = model.Consumption;
            Emission = model.Emission;
            Gears = model.Gears;
            Price = model.Price;
            TopSpeed = model.TopSpeed;
            WheelDrive = model.WheelDrive;
            Engine = model.Engine;
        }
    }
}