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
        public double Liter { get; }
        public int Performance { get; }
        public int Size { get; }
        public EngineKind EngineKind { get; }

        public EngineViewModel(Engine model)
        {
            Id = model.Id;
            EngineKind = model.EngineKind;
            Liter = model.Liter;
            Performance = model.Performance;
            Size = model.Size;
        }
    }
}