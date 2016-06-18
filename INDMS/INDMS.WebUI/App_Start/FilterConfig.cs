using INDMS.WebUI.Infrastructure.Filters;
using System.Web.Mvc;

namespace INDMS.WebUI.App_Start {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new ExceptionHandlerAttribute());
        }
    }
}