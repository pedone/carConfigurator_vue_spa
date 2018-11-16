using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; }
        public double BasePrice { get; }
        public BodyKind BodyType { get; }
        public CarBrandKind Brand { get; }
        public WheelDriveKind WheelDrive { get; }
        public string Model { get; }

        public CarViewModel(Car model)
        {
            Id = model.Id;
            BasePrice = model.BasePrice;
            BodyType = model.BodyType;
            Brand = model.Brand;
            WheelDrive = model.WheelDrive;
            Model = model.Model;
        }
    }
}