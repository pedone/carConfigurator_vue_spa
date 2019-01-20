using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ViewModelConfiguration
    {
        public CarModelViewModel CarModel { get; set; }
        public int EngineSettingsId { get; set; } = -1;
        public int PaintId { get; set; } = -1;
        public int RimId { get; set; } = -1;

        private IEnumerable<int> _AccessoryIds;
        public IEnumerable<int> AccessoryIds
        {
            get => _AccessoryIds ?? Enumerable.Empty<int>();
            set => _AccessoryIds = value;
        }

        public void Reset()
        {
            CarModel = null;
            EngineSettingsId = -1;
            PaintId = -1;
            RimId = -1;
            AccessoryIds = null;
        }
    }
}