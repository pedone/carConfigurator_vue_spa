using KFZ_Konfigurator.Models;

namespace KFZ_Konfigurator.ViewModels
{
    public class EngineViewModel : ViewModelBase
    {
        public EngineKind EngineKind { get; }
        public int Liter { get; }
        public int Performance { get; }
        public int Size { get; }

        public EngineViewModel(Engine model)
            : base(model.Id)
        {
            EngineKind = model.EngineKind;
            Liter = model.Liter;
            Performance = model.Performance;
            Size = model.Size;
        }
    }
}