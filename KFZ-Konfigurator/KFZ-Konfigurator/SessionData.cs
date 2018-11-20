using KFZ_Konfigurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator
{
    public static class SessionData
    {
        public static ViewModelConfiguration ActiveConfiguration
        {
            get => GetValue<ViewModelConfiguration>("Configuration") ?? SetValue("Configuration", new ViewModelConfiguration());
            set => SetValue("Configuration", value);
        }

        private static T GetValue<T>(string name)
        {
            var result = HttpContext.Current.Session[name];
            return result != null ? (T)result : default(T);
        }
        private static T SetValue<T>(string name, T value)
        {
            HttpContext.Current.Session[name] = value;
            return value;
        }
    }
}