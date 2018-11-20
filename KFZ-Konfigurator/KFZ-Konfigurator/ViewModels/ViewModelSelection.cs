using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ViewModelSelection<T> where T : ViewModelBase
    {
        public List<T> Selection { get; }
        public IEnumerable<T> Items { get; }

        public ViewModelSelection(IEnumerable<T> items, IEnumerable<T> selection = null)
        {
            Items = items.ToList();
            Selection = selection?.ToList() ?? new List<T>();
        }
    }
}