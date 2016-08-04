using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers
{
    public class PhotographsController : Controller
    {
        // GET: Photographs
        public ActionResult Index()
        {
            return View();
        }
    }
}