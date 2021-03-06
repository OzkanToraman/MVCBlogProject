﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MVC.Blog.Project.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {


            #region AdminBundles
            bundles.Add(new StyleBundle("~/bundlesLoginv2/styles")
                    .IncludeDirectory(
                    "~/Content/Adminv2/css",
                    "*.css",
                    true));
            bundles.Add(new ScriptBundle("~/bundlesLoginv2/scripts")
                .IncludeDirectory(
                "~/Content/Adminv2/js",
                "*.js", true));
            #endregion

            #region SiteBundles
            bundles.Add(new StyleBundle("~/bundlesSite/styles")
                  .IncludeDirectory(
                  "~/Content/Site/css",
                  "*.css",
                  true));
            bundles.Add(new ScriptBundle("~/bundlesSite/scripts")
                .IncludeDirectory(
                "~/Content/Site/js",
                "*.js", true)); 
            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}