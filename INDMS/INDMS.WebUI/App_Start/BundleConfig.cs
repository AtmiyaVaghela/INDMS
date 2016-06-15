using System.Web.Optimization;

namespace INDMS.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.min.css",
                "~/plugins/font-awesome-4.6.2/css/font-awesome.min.css",
                "~/plugins/ionicons-2.0.1/css/ionicons.min.css",
                "~/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                 "~/plugins/select2/select2.min.css",
                "~/Content/AdminLTE.min.css",
                "~/Content/skins/_all-skins.min.css",               
                "~/plugins/datepicker/datepicker3.css",
                "~/plugins/datatables/dataTables.bootstrap.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/plugins/jQuery/jQuery-2.1.4.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/plugins/datepicker/bootstrap-datepicker.js",
                "~/plugins/select2/select2.full.min.js",
                "~/plugins/datatables/jquery.dataTables.min.js",
                "~/plugins/datatables/dataTables.bootstrap.min.js",
                "~/plugins/fastclick/fastclick.min.js",
                "~/Scripts/app.js",
                "~/plugins/sparkline/jquery.sparkline.min.js",
                "~/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                "~/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                "~/plugins/slimScroll/jquery.slimscroll.min.js",
                "~/plugins/chartjs/Chart.min.js",
                 "~/Scripts/jquery.cookie-1.4.1.min.js"));
        }
    }
}