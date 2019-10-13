using System.Web;
using System.Web.Optimization;

namespace EpicGameWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
               new Bundle("~/Bundles/AngularStyles")
               .Include(
                   "~/Scripts/Client/styles-es5.js",
                   "~/Scripts/Client/styles-es2015.js"
               ));
        }
    }
}
