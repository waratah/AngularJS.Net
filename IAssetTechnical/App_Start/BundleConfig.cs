﻿using System.Web;
using System.Web.Optimization;

namespace IAssetTechnical
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js")
                .IncludeDirectory("~/Scripts/angular-ui", "*.js", true)
                        );



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            var app = new ScriptBundle("~/bundles/js-app")
                    .Include("~/Areas/Core/TypeScript/mx-google-analytics.js")
                    .Include("~/Areas/Core/TypeScript/Events.js");
            bundles.Add(app);
        }

    }
}
