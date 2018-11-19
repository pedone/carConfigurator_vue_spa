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
        public string Model { get; }

        public CarViewModel(CarModel model)
        {
            Id = model.Id;
            BodyType = model.BodyType;
            //Brand = model.Brand;
            //Model = model.Model;
            //BasePrice = model.Engines.Min(cur => cur.Price);
        }
    }
}