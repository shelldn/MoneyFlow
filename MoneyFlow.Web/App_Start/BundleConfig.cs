using System.Web.Optimization;

namespace MoneyFlow.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region css
            bundles.Add(new StyleBundle("~/client/css/style").Include(
                "~/client/css/vendor/normalize.css",
                "~/client/css/style.css"
            ));
            #endregion

            #region js
            bundles.Add(new ScriptBundle("~/bundle/moment")
                .Include("~/client/js/vendor/moment/moment.js"));

            bundles.Add(new ScriptBundle("~/bundle/underscore")
                .Include("~/client/js/vendor/underscore/underscore.js"));

            bundles.Add(new Bundle("~/bundle/jquery")
                .Include("~/client/js/vendor/jquery/dist/jquery.js"));

            bundles.Add(new Bundle("~/bundle/angular").Include(
                "~/client/js/vendor/angular/angular.js",
                "~/client/js/vendor/angular-resource/angular-resource.js",
                "~/client/js/vendor/angular-messages/angular-messages.js",
                "~/client/js/vendor/angular-local-storage/dist/angular-local-storage.js"
            ));

            bundles.Add(new Bundle("~/bundle/app").Include(
                "~/client/js/data.js",
                "~/client/js/auth.js",
                "~/client/js/controls.js"
            ));
            #endregion
        }
    }
}