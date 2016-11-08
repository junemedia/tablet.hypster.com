﻿using System.Web;
using System.Web.Optimization;

namespace hypster
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region TMP_DEL
            /*
            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui*"));
            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));
            //bundles.Add(new StyleBundle("~/css/themes/base/css").Include(
            //            "~/css/themes/base/jquery.ui.core.css",
            //            "~/css/themes/base/jquery.ui.resizable.css",
            //            "~/css/themes/base/jquery.ui.selectable.css",
            //            "~/css/themes/base/jquery.ui.accordion.css",
            //            "~/css/themes/base/jquery.ui.autocomplete.css",
            //            "~/css/themes/base/jquery.ui.button.css",
            //            "~/css/themes/base/jquery.ui.dialog.css",
            //            "~/css/themes/base/jquery.ui.slider.css",
            //            "~/css/themes/base/jquery.ui.tabs.css",
            //            "~/css/themes/base/jquery.ui.datepicker.css",
            //            "~/css/themes/base/jquery.ui.progressbar.css",
            //            "~/css/themes/base/jquery.ui.theme.css"));
            */
            #endregion


            //used on create playlist and player page
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //"~/Scripts/jquery-1.*"));


            bundles.Add(new StyleBundle("~/css/css").Include(
                "~/css/site.css",
                "~/css/Player.css",
                "~/css/home/Home.css",
                "~/css/home/Widget_MostPopularSongs.css",
                "~/css/home/GetSideVideos.css"
                ));
            
        }
    }
}