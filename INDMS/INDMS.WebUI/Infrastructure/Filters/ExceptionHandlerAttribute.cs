using INDMS.WebUI.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace INDMS.WebUI.Infrastructure.Filters {

    public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter {

        public void OnException(ExceptionContext filterContext) {
            if (!filterContext.ExceptionHandled) {
                ExceptionLogger logger = new ExceptionLogger() {
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionStackTrace = filterContext.Exception.StackTrace,
                    ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                    LogTime = DateTime.Now
                };

                WriteToFile(logger);

                filterContext.ExceptionHandled = true;
            }
        }

        private void WriteToFile(ExceptionLogger logger) {
            string path = HttpContext.Current.Server.MapPath("/Logs/") + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            using (StreamWriter w = new StreamWriter(path, true)) {
                w.WriteLine(string.Format("{0} | {1} | {2} | {3}", logger.LogTime, logger.ControllerName, logger.ExceptionMessage, logger.ExceptionStackTrace));
                w.Close();
            }
        }
    }
}