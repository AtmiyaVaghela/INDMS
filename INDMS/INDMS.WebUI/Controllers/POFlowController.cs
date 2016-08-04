using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;
using INDMS.WebUI.Infrastructure.Filters;

namespace INDMS.WebUI.Controllers
{
    public class POFlowController : Controller
    {
        // GET: POFlow
        [AuthUser]
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
                    m.POGeneration = ctx.POGenerations.Where(x => x.PO_ID == Id).FirstOrDefault();
                    m.PO = ctx.PurchaseOrders.Find(Id);
                    m.FCL = ctx.FCLs.FirstOrDefault(x => x.POId == m.PO.Id);
                    m.QAP = ctx.QAPs.FirstOrDefault(x => x.POId == m.PO.Id);

                    string[] Inspectors = m.PO.Inspectors.Split(',');
                    m.PO.Inspectors = string.Empty;
                    foreach (var ins in Inspectors)
                    {
                        m.PO.Inspectors += ctx.Users.SingleOrDefault(z => z.UserId == new Guid(ins)).Name + ",";
                    }

                    m.PO.Inspectors.Substring(0, m.PO.Inspectors.Length - 1);

                    m.PO.FirmAddress = ctx.Firms.SingleOrDefault(z => z.Id.ToString() == m.PO.Firm).FirmAddress;

                    m.PO.Firm = ctx.Firms.SingleOrDefault(z => z.Id.ToString() == m.PO.Firm).FirmName;

                    if (m.QAP != null)
                    {
                       
                            m.QAP.ApprovedBy = ctx.Users.SingleOrDefault(x => x.UserId == new Guid(m.QAP.ApprovedBy)).Name;
                            if (m.QAP.DrawingNoRef != null)
                            {
                                string[] d = m.QAP.DrawingNoRef.Split(',');

                                m.QAP.DrawingNoRef = string.Empty;

                                foreach (var i in d)
                                {
                                    m.QAP.DrawingNoRef += ctx.Drawings.ToList().SingleOrDefault(x => x.Id == Convert.ToInt32(i)).DrawingNo + " ,";
                                }

                                m.QAP.DrawingNoRef = m.QAP.DrawingNoRef.Substring(0, m.QAP.DrawingNoRef.Length - 1);
                            }

                       
                    }
                }
                if (m == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {

                    return View(m);
                }
            }
        }
    }
}