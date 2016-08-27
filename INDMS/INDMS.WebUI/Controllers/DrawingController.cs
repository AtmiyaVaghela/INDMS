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
    public class DrawingController : Controller
    {
        [AuthUser]
        public ActionResult New(int? Id)
        {
            DrawingViewModel m = new DrawingViewModel();

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

                    if (m.PO != null)
                    {
                        m.Drawing = new Drawing();
                        m.Drawing.POId = Id ?? 0;
                        m.Drawing.PONo = m.PO.PONo;
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

        [AuthUser]
        [HttpPost]
        public ActionResult New(DrawingViewModel m, HttpPostedFileBase inputFile)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(m.PO.Id)))
            {
                if (!string.IsNullOrEmpty(m.Drawing.PONo))
                {
                    if (!string.IsNullOrEmpty(m.Drawing.FileNo))
                    {
                        if (!string.IsNullOrEmpty(m.Drawing.Subject))
                        {
                            if (!string.IsNullOrEmpty(m.Drawing.ApprovalBy))
                            {
                                if (inputFile != null && inputFile.ContentLength > 0)
                                {
                                    if (inputFile.ContentType == "application/pdf")
                                    {
                                        Guid FileName = Guid.NewGuid();
                                        m.Drawing.FilePath = "/Uploads/Drawings/" + FileName + ".pdf";
                                        string tPath = Path.Combine(Server.MapPath("~/Uploads/Drawings/"), FileName + ".pdf");
                                        inputFile.SaveAs(tPath);

                                        m.Drawing.POId = m.PO.Id;
                                        m.Drawing.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                        m.Drawing.CreatedDate = DateTime.Now;

                                        using (var ctx = new INDMSEntities())
                                        {
                                            ctx.Drawings.Add(m.Drawing);
                                            ctx.SaveChanges();
                                        }

                                        TempData["RowId"] = m.Drawing.Id;
                                        TempData["MSG"] = "Save Successfully";

                                        return RedirectToAction("Index", "POFlow", new { id = m.PO.Id });
                                    }
                                    else
                                    {
                                        TempData["Error"] = "Please Select PDF Files Only.";
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = "Please Select File";
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Please enter Approved By";
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please enter Description";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Please enter File No";
                    }
                }
                else
                {
                    TempData["Error"] = "Please enter PO Name.";
                }
            }
            else
            {
                TempData["Error"] = "PO Id is not assign.";
            }

            return View();
        }

        [AuthUser]
        public ActionResult Edit(int? Id)
        {
            DrawingViewModel m = new DrawingViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.Drawing = ctx.Drawings.Find(Id);
                    if (m.Drawing != null)
                    {
                        m.file = m.Drawing.FilePath;
                        m.POGeneration = ctx.POGenerations.Where(x => x.PO_ID == m.Drawing.POId).SingleOrDefault();
                        m.PO = ctx.PurchaseOrders.Find(m.Drawing.POId);
                        return View(m);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
            }
        }

        [AuthUser]
        [HttpPost]
        public ActionResult Edit(DrawingViewModel m, HttpPostedFileBase inputFile)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(m.PO.Id)))
            {
                if (!string.IsNullOrEmpty(m.Drawing.PONo))
                {
                    if (!string.IsNullOrEmpty(m.Drawing.FileNo))
                    {
                        if (!string.IsNullOrEmpty(m.Drawing.Subject))
                        {
                            if (!string.IsNullOrEmpty(m.Drawing.ApprovalBy))
                            {
                                if (inputFile != null && inputFile.ContentLength > 0)
                                {
                                    if (inputFile.ContentType == "application/pdf")
                                    {
                                        Guid FileName = Guid.NewGuid();
                                        m.Drawing.FilePath = "/Uploads/Drawings/" + FileName + ".pdf";
                                        string tPath = Path.Combine(Server.MapPath("~/Uploads/Drawings/"), FileName + ".pdf");
                                        inputFile.SaveAs(tPath);
                                    }
                                    else
                                    {
                                        TempData["Error"] = "Only PDF Files.";
                                    }
                                }
                                else
                                {
                                    m.Drawing.FilePath = m.file;
                                }

                                m.Drawing.POId = m.PO.Id;
                                m.Drawing.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                m.Drawing.CreatedDate = DateTime.Now;

                                try
                                {
                                    using (var db = new INDMSEntities())
                                    {
                                        db.Entry(m.Drawing).State = EntityState.Modified;
                                        db.SaveChanges();
                                        ModelState.Clear();
                                    }

                                    TempData["RowId"] = m.Drawing.Id;
                                    TempData["MSG"] = "Save Successfully";

                                    return RedirectToAction("Index", "POFlow", new { id = m.PO.Id });
                                }
                                catch (Exception ex)
                                {
                                    TempData["Error"] = ex.Message;
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Please enter Approved By";
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please enter Description";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Please enter File No";
                    }
                }
                else
                {
                    TempData["Error"] = "Please enter PO Name.";
                }
            }
            else
            {
                TempData["Error"] = "PO Id is not assign.";
            }

            return View();
        }
    }
}