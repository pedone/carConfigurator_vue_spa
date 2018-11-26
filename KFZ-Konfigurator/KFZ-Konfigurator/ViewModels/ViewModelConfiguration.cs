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

        public CarModelViewModel CarModel { get; set; }
        public int EngineSettingsId { get; set; } = -1;
        public int PaintId { get; set; } = -1;
        public int RimId { get; set; } = -1;

        public int[] AccessoryIds { get; set; }

        /// <summary>
        /// Reset everything except for the car model
        /// </summary>
        public void Reset()
        {
            EngineSettingsId = -1;
            PaintId = -1;
            RimId = -1;
            AccessoryIds = null;
        }
    }
}