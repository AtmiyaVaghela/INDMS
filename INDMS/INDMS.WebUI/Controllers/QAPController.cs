using INDMS.WebUI.Infrastructure.Filters;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers
{
    public class QAPController : Controller
    {
        // GET: QAP
        [AuthUser]
        public ActionResult New(int? Id)
        {
            QAPViewModel m = new QAPViewModel();

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
                    if (m.PO != null)
                    {
                        m.QAP = new QAP();
                        m.QAP.POId = Id ?? 0;
                        m.QAP.PONo = m.PO.PONo;
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthUser]
        public ActionResult New(QAPViewModel m, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var ctx = new INDMSEntities())
                    {
                        m.POGeneration = ctx.POGenerations.Where(x => x.PO_ID == m.QAP.POId).FirstOrDefault();
                        m.PO = ctx.PurchaseOrders.Find(m.QAP.POId);
                        if (m.PO != null)
                        {
                            m.QAP.PONo = m.PO.PONo;
                        }
                    }

                    if (m.PO != null)
                    {
                        if (!string.IsNullOrEmpty(m.QAP.QAPNo))
                        {
                            if (!string.IsNullOrEmpty(m.QAP.Subject))
                            {
                                if (!string.IsNullOrEmpty(m.QAP.ApprovedBy))
                                {
                                    if (inputFile != null && inputFile.ContentLength > 0)
                                    {
                                        if (inputFile.ContentType == "application/pdf")
                                        {
                                            Guid FileName = Guid.NewGuid();
                                            m.QAP.FilePath = "/Uploads/QAP/" + FileName + ".pdf";
                                            string tPath = Path.Combine(Server.MapPath("~/Uploads/QAP/"), FileName + ".pdf");
                                            inputFile.SaveAs(tPath);

                                            if (m.DrawingId != null)
                                            {
                                                foreach (var i in m.DrawingId)
                                                {
                                                    m.QAP.DrawingNoRef += i + ",";
                                                }
                                                m.QAP.DrawingNoRef = m.QAP.DrawingNoRef.Substring(0, m.QAP.DrawingNoRef.Length - 1);
                                            }

                                            m.QAP.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                            m.QAP.CreatedDate = null;
                                            using (var ctx = new INDMSEntities())
                                            {
                                                ctx.QAPs.Add(m.QAP);
                                                ctx.SaveChanges();
                                            }

                                            TempData["RowId"] = m.QAP.Id;
                                            TempData["MSG"] = "Save Successfully";

                                            return RedirectToAction("Index", "POFlow", new { id = m.QAP.POId });
                                        }
                                        else
                                        {
                                            TempData["Error"] = "Please select only PDF file.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["Error"] = "Please Select File";
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = "Please Enter Approved By";
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Please Enter Subject";
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please Enter QAP No.";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "PO Not Found.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            return View(m);
        }

        // GET: QAP
        [AuthUser]
        public ActionResult Edit(int? Id)
        {
            QAPViewModel m = new QAPViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.QAP = ctx.QAPs.Find(Id);

                    if (m.QAP != null)
                    {
                        if (m.QAP.DrawingNoRef != null)
                        {
                            string[] s = m.QAP.DrawingNoRef.Split(',');
                            m.DrawingId = new Int32[s.Length];
                            for (int i = 0; i < s.Length; i++)
                            {
                                m.DrawingId[i] = Convert.ToInt32(s[i]);
                            }
                        }

                        m.file = m.QAP.FilePath;
                        m.POGeneration = ctx.POGenerations.Where(x => x.PO_ID == m.QAP.POId).FirstOrDefault();

                        m.PO = ctx.PurchaseOrders.Find(m.QAP.POId);

                        if (m.PO != null)
                        {
                            m.QAP.PONo = m.PO.PONo;
                        }
                        else
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                    }
                }
                if (m.QAP == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    return View(m);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthUser]
        public ActionResult Edit(QAPViewModel m, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if ((m.QAP.POId ?? 0) > 0)
                    {
                        if (!string.IsNullOrEmpty(m.QAP.QAPNo))
                        {
                            if (!string.IsNullOrEmpty(m.QAP.Subject))
                            {
                                if (!string.IsNullOrEmpty(m.QAP.ApprovedBy))
                                {
                                    if (inputFile != null && inputFile.ContentLength > 0)
                                    {
                                        if (inputFile.ContentType == "application/pdf")
                                        {
                                            Guid FileName = Guid.NewGuid();
                                            m.QAP.FilePath = "/Uploads/QAP/" + FileName + ".pdf";
                                            string tPath = Path.Combine(Server.MapPath("~/Uploads/QAP/"), FileName + ".pdf");
                                            inputFile.SaveAs(tPath);
                                        }
                                        else
                                        {
                                            TempData["Error"] = "Please select only PDF file.";
                                        }
                                    }
                                    else
                                    {
                                        m.QAP.FilePath = m.file;
                                    }

                                    if (m.DrawingId != null)
                                    {
                                        foreach (var i in m.DrawingId)
                                        {
                                            m.QAP.DrawingNoRef += i + ",";
                                        }
                                        m.QAP.DrawingNoRef = m.QAP.DrawingNoRef.Substring(0, m.QAP.DrawingNoRef.Length - 1);
                                    }

                                    m.QAP.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                    m.QAP.CreatedDate = DateTime.Now;

                                    using (var ctx = new INDMSEntities())
                                    {
                                        ctx.Entry(m.QAP).State = EntityState.Modified;
                                        ctx.SaveChanges();

                                        m.POGeneration = ctx.POGenerations.FirstOrDefault(x => x.PO_ID == m.QAP.POId);
                                        if (m.POGeneration != null)
                                        {
                                            m.POGeneration.I_NOTE = 1;

                                            ctx.Entry(m.POGeneration).State = EntityState.Modified;
                                            ctx.SaveChanges();
                                        }
                                    }

                                    TempData["RowId"] = m.QAP.Id;
                                    TempData["MSG"] = "Save Successfully";

                                    return RedirectToAction("Index", "POFlow", new { id = m.QAP.POId });
                                }
                                else
                                {
                                    TempData["Error"] = "Please Enter Approved By";
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Please Enter Subject";
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please Enter QAP No.";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "PO Not Found.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }

            return View(m);
        }
    }
}