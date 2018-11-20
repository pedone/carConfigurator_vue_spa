using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ViewModelConfiguration
    {
        private List<ViewModelBase> _items = new List<ViewModelBase>();

        /// <summary>
        /// Get the accumulated price of all items
        /// </summary>
        public double Price
        {
            get => _items.Select(cur => cur.Price).Aggregate((next, result) => result + next);
        }

        public IEnumerable<T> Get<T>()
        {
            return _items.OfType<T>();
        }

        public void Reset()
        {
            _items.Clear();
        }

        public void Add(ViewModelBase item)
        {
            if (!Contains(item))
                _items.Add(item);
        }

        private bool Contains(ViewModelBase item)
        {
            return _items.Any(cur => cur.GetType() == item.GetType() && cur.Id == item.Id);
        }
    }
}