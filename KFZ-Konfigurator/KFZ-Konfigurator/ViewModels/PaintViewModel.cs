using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class PaintViewModel
    {
        public int Id { get; }
        public PaintCategory Category { get; }
        public string Color { get; }
        public string Name { get; }
        public double Price { get; }

        public PaintViewModel(Paint model)
        {
            Id = model.Id;
            Category = model.Category;
            Color = model.Color;
            Name = model.Name;
            Price = model.Price;
        }
    }
}