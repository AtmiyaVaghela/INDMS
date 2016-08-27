using INDMS.WebUI.Infrastructure.Filters;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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

                    //For PO
                    m.PO = ctx.PurchaseOrders.Find(Id);

                    string[] Inspectors = m.PO.Inspectors.Split(',');
                    m.PO.Inspectors = string.Empty;
                    foreach (var ins in Inspectors)
                    {
                        m.PO.Inspectors += ctx.Users.SingleOrDefault(z => z.UserId == new Guid(ins)).Name + ",";
                    }

                    m.PO.Inspectors.Substring(0, m.PO.Inspectors.Length - 1);

                    m.PO.FirmAddress = ctx.Firms.SingleOrDefault(z => z.Id.ToString() == m.PO.Firm).FirmAddress;

                    m.PO.Firm = ctx.Firms.SingleOrDefault(z => z.Id.ToString() == m.PO.Firm).FirmName;

                    //For FCL
                    m.FCLs = (from d in ctx.FCLs
                              where d.POId == m.PO.Id
                              select d).ToList();

                    //For QAP
                    m.QAPs = ctx.QAPs.Where(x => x.POId == m.PO.Id).ToList();

                    foreach (QAP QAP in m.QAPs)
                    {
                        QAP.ApprovedBy = ctx.Users.SingleOrDefault(x => x.UserId == new Guid(QAP.ApprovedBy)).Name;
                        if (QAP.DrawingNoRef != null)
                        {
                            string[] d = QAP.DrawingNoRef.Split(',');

                            QAP.DrawingNoRef = string.Empty;

                            foreach (var i in d)
                            {
                                QAP.DrawingNoRef += ctx.Drawings.ToList().SingleOrDefault(x => x.Id == Convert.ToInt32(i)).DrawingNo + " ,";
                            }

                            QAP.DrawingNoRef = QAP.DrawingNoRef.Substring(0, QAP.DrawingNoRef.Length - 1);
                        }
                    }

                    //For Drawings
                    m.Drawings = ctx.Drawings.Where(x => x.POId == Id).ToList();

                    //For I-Note
                    m.CoveringLetters = ctx.CoveringLetters.Where(x => x.POId == Id).ToList();

                    //For PO Correspondence
                    m.POCorrespondences = ctx.POCorrespondences.Where(x => x.POId == Id).ToList();
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