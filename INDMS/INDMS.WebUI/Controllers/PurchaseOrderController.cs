using INDMS.WebUI.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers
{
    public class PurchaseOrderController : Controller
    {
        // GET: PurchaseOrder
        [AuthUser]
        public ActionResult Index()
        {
            return View();
        }

        [AuthUser]
        public ActionResult New()
        {
            return View();
        }
    }
}