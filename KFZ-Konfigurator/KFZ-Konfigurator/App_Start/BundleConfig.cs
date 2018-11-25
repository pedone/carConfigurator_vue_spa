using System.Web;
using System.Web.Optimization;

namespace KFZ_Konfigurator
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapKnockout").Include(
                      "~/Scripts/lib/bootstrap.js",
                      "~/Scripts/lib/knockout-{version}.js",
                      "~/Scripts/lib/underscore-{version}.js",
                      "~/Scripts/lib/popper.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/lib/bootstrap.css",
                      "~/Content/lib/site.css",
                      "~/Content/lib/app/styles.css"));
        }
    }
}
