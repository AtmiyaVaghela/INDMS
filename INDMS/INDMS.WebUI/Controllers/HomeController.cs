using INDMS.WebUI.Infrastructure.Filters;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AuthUser]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}