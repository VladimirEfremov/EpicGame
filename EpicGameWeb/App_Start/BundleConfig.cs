using System.Web;
using System.Web.Optimization;

namespace EpicGameWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Script/Bundle")
                .Include(
                "~/Scripts/Client/runtime.*",
                "~/Scripts/Client/polyfills.*",
                "~/Scripts/Client/styles.*",
                "~/Scripts/Client/vendor.*",
                "~/Scripts/Client/scripts.*",
                "~/Scripts/Client/main.*"));
        }
    }
}
