using KFZ_Konfigurator.Models;
using System;

namespace KFZ_Konfigurator.ViewModels
{
    public class EngineViewModel : ItemViewModelBase
    {
        public EngineKind EngineKind { get; }
        public int Liter { get; }
        public int Size { get; }
        public int Performance { get; }
        public int PerformancePS { get; }

        public EngineViewModel(Engine model)
            : base(model.Id)
        {
            EngineKind = model.EngineKind;
            Liter = model.Liter;
            Size = model.Size;
            Performance = model.Performance;
            PerformancePS = (int)Math.Round(model.Performance * Constants.KW_to_PS);
        }
    }
}