using System.Web;
using System.Web.Optimization;

namespace JsMiracle.WebUI
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.easyui-{version}.js",
                        "~/Scripts/moment*"
                        //,"~/Scripts/knockout-3.4.0*"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
            //    "~/Scripts/jquery.easyui-{version}.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/themes/default/easyui.css",
                "~/Content/themes/icon.css",
                "~/Content/queryBuilderStyles.css"
            ));


            //bundles.Add(new StyleBundle("~/EasyUI/themes/base").Include(
            //"~/Content/themes/default/easyui.css",
            //"~/Content/themes/icon.css"));

            //// 日期格式化js
            //bundles.Add(new ScriptBundle("~/bundles/moment").Include(
            //       "~/Scripts/moment*"));

        }
    }
}