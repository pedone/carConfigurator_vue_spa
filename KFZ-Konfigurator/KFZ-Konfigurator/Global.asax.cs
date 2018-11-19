using KFZ_Konfigurator.Models;
using KFZ_Konfigurator.Models.SeedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KFZ_Konfigurator
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static string ApplicationProductTitle { get; private set; }

        public static ConfigurationReference ActiveConfiguration
        {
            get
            {
                if (HttpContext.Current.Session["Configuration"] == null)
                    HttpContext.Current.Session["Configuration"] = new ConfigurationReference();
                return (ConfigurationReference)HttpContext.Current.Session["Configuration"];
            }
            set => HttpContext.Current.Session["Configuration"] = value;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // init db
            SeedData.Initialize();

            var productTitleAttribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), true).First();
            ApplicationProductTitle = ((AssemblyProductAttribute)productTitleAttribute).Product;
        }
    }
}
