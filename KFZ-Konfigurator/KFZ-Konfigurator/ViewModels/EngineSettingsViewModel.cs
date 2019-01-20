using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class EngineSettingsViewModel : ItemViewModelBase
    {
        public double Acceleration { get; }
        public double Consumption { get; }
        public int Emission { get; }
        public int Gears { get; }
        public int TopSpeed { get; }
        public WheelDriveKind WheelDrive { get; }
        public EngineViewModel Engine { get; }

        public EngineSettingsViewModel(EngineSettings model)
            : base(model.Id, model.Price)
        {
            Acceleration = model.Acceleration;
            Consumption = model.Consumption;
            Emission = model.Emission;
            Gears = model.Gears;
            TopSpeed = model.TopSpeed;
            WheelDrive = model.WheelDrive;
            Engine = new EngineViewModel(model.Engine);
        }
    }
}