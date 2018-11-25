using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ExteriorPageViewModel
    {
        public IEnumerable<PaintViewModel> Paints { get; set; }
    }
}