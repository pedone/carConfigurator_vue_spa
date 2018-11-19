using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class EngineViewModel
    {
        public int Id { get; }
        public double Acceleration { get; }
        public double Consumption { get; }
        public int Emission { get; }
        public double EngineSize { get; }
        public double Liter { get; }
        public int Performance { get; }
        public double Price { get; }
        public int TopSpeed { get; }
        public EngineKind EngineKind { get; }
        public int Gears { get; }
        public WheelDriveKind WheelDrive { get; }

        public EngineViewModel(Engine model)
        {
            Id = model.Id;
            //Acceleration = model.Acceleration;
            //Consumption = model.Consumption;
            //Emission = model.Emission;
            //EngineSize = model.EngineSize;
            //Liter = model.Liter;
            //Performance = model.Performance;
            //Price = model.Price;
            //TopSpeed = model.TopSpeed;
            //EngineKind = model.EngineKind;
            //Gears = model.Gears;
            //WheelDrive = model.WheelDrive;
        }
    }
}