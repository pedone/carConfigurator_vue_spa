using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class RimViewModel : ViewModelBase
    {
        public int Size { get; }

        public RimViewModel(Rim model)
            : base(model.Id, model.Price)
        {
            Size = model.Size;
        }
    }
}