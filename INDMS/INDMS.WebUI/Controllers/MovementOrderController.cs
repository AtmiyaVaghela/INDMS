using INDMS.WebUI.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers
{
    [ExceptionHandler]
    public class MovementOrderController : Controller
    {
        // GET: MovementOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New() {
            return View();
        }
    }
}