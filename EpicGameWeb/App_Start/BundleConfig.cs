using System.Web;
using System.Web.Optimization;

namespace EpicGameWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new Bundle("~/Bundles/Angular")
                .Include(
                    "~/Scripts/Client/polyfills-es5.js",
                    "~/Scripts/Client/polyfills-es2015.js",
                    "~/Scripts/Client/scripts.js",
                    "~/Scripts/Client/runtime-es2015.js",
                    "~/Scripts/Client/vendor-es2015.js",
                    "~/Scripts/Client/main-es2015.js",
                    "~/Scripts/Client/runtime-es5.js",
                    "~/Scripts/Client/vendor-es5.js",
                    "~/Scripts/Client/main-es5.js"
                ));
        }
    }
}
