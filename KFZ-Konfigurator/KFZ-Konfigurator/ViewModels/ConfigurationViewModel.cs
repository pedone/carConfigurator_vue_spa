using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        public string Name { get; }
        public string Guid { get; }

        public ConfigurationViewModel(Configuration model)
            : base(model.Id, model.Price)
        {
            Name = model.Name;
            Guid = model.Guid;
        }
    }
}