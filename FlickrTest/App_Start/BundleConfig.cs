using System.Web.Optimization;

namespace FlickrTest.App_Start
{
    /// <summary>
    /// Handles bundle configuration
    /// </summary>
    public class BundleConfig
    {

        public const string Css = "~/Content/css";
        public const string Scripts = "~/Content/js";
        public const string Jquery = "~/Content/Jquery";
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle(Jquery).Include(
            "~/Scripts/jquery-{version}.js"
            ));

            bundles.Add(new ScriptBundle(Scripts).Include(

                        "~/Scripts/ui.js",
                        "~/Scripts/ImageLoader.js",
                        "~/Scripts/DataCache.js",
                                                "~/Scripts/Scripts.js"
                        ));

            bundles.Add(new StyleBundle(Css).Include("~/Content/site.css"));

        }
    }
}