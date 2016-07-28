using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;

namespace INDMS.WebUI.Controllers
{
    public class POFlowController : Controller
    {
        // GET: POFlow
        public ActionResult Index(int? Id)
        {
            POFlowViewModel m = new POFlowViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.POGeneration = ctx.POGenerations.Where(x => x.PO_ID == Id).SingleOrDefault();
                    m.PO = ctx.PurchaseOrders.Find(Id);
                }
                if (m == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                    return View(m);

            }
        }
    }
}