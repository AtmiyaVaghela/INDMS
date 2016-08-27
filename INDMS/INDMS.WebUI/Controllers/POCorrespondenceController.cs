using INDMS.WebUI.Infrastructure.Filters;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers
{
    public class POCorrespondenceController : Controller
    {
        // GET: POCorrespondence
        [AuthUser]
        public ActionResult New(int? Id)
        {
            POCorrespondenceViewModel m = new POCorrespondenceViewModel();

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
                        m.POCorrespondence = new POCorrespondence();
                        m.POCorrespondence.POId = Id ?? 0;
                        m.POCorrespondence.PONo = m.PO.PONo;
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
        [AuthUser]
        [ValidateAntiForgeryToken]
        public ActionResult New(POCorrespondenceViewModel m, HttpPostedFileBase inputFile)
        {
            if ((m.POCorrespondence.POId ?? 0) > 0)
            {
                if (!string.IsNullOrEmpty(m.POCorrespondence.PONo))
                {
                    if (!string.IsNullOrEmpty(m.POCorrespondence.FileNo))
                    {
                        if (!string.IsNullOrEmpty(m.POCorrespondence.LetterNo))
                        {
                            if (!string.IsNullOrEmpty(m.POCorrespondence.Description))
                            {
                                if (!string.IsNullOrEmpty(m.POCorrespondence.Subject))
                                {
                                    if (!string.IsNullOrEmpty(m.POCorrespondence.TypeOfCorrespondence))
                                    {
                                        if (m.POCorrespondence.TypeOfCorrespondence == "IN")
                                        {
                                            if (!string.IsNullOrEmpty(m.POCorrespondence.From))
                                            {
                                                m.POCorrespondence.To = string.Empty;
                                            }
                                            else
                                            {
                                                TempData["Error"] = "Please enter From.";
                                                return View(m);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(m.POCorrespondence.To))
                                            {
                                                m.POCorrespondence.From = string.Empty;
                                            }
                                            else
                                            {
                                                TempData["Error"] = "Please enter To.";
                                                return View(m);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(m.POCorrespondence.CopyTo))
                                        {
                                            if (inputFile != null && inputFile.ContentLength > 0)
                                            {
                                                if (inputFile.ContentType == "application/pdf")
                                                {
                                                    Guid FileName = Guid.NewGuid();
                                                    m.POCorrespondence.FilePath = "/Uploads/POCorrespondence/" + FileName + ".pdf";
                                                    string tPath = Path.Combine(Server.MapPath("~/Uploads/POCorrespondence/"), FileName + ".pdf");
                                                    inputFile.SaveAs(tPath);

                                                    m.POCorrespondence.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                                    m.POCorrespondence.CreatedDate = DateTime.Now;

                                                    using (var ctx = new INDMSEntities())
                                                    {
                                                        ctx.POCorrespondences.Add(m.POCorrespondence);
                                                        ctx.SaveChanges();
                                                    }

                                                    TempData["RowId"] = m.POCorrespondence.Id;
                                                    TempData["MSG"] = "Save Successfully";

                                                    return RedirectToAction("Index", "POFlow", new { id = m.POCorrespondence.POId });
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
                                            TempData["Error"] = "Please Enter Copy To.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["Error"] = "Please select type of Correspondence.";
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = "Please enter Subject";
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Please enter Description";
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please enter Letter No";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Please enter File No";
                    }
                }
                else
                {
                    TempData["Error"] = "Please enter PO No";
                }
            }
            else
            {
                TempData["Error"] = "PO is not assign.";
            }
            return View(m);
        }

        [AuthUser]
        public ActionResult Edit(int? Id)
        {
            POCorrespondenceViewModel m = new POCorrespondenceViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.POCorrespondence = ctx.POCorrespondences.Find(Id);
                    if (m.POCorrespondence != null)
                    {
                        m.file = m.POCorrespondence.FilePath;
                        m.POGeneration = ctx.POGenerations.Where(x => x.PO_ID == m.POCorrespondence.POId).SingleOrDefault();
                        m.PO = ctx.PurchaseOrders.Find(m.POCorrespondence.POId);
                        return View(m);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
            }
        }

        [HttpPost]
        [AuthUser]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(POCorrespondenceViewModel m, HttpPostedFileBase inputFile)
        {
            if ((m.POCorrespondence.POId ?? 0) > 0)
            {
                if (!string.IsNullOrEmpty(m.POCorrespondence.PONo))
                {
                    if (!string.IsNullOrEmpty(m.POCorrespondence.FileNo))
                    {
                        if (!string.IsNullOrEmpty(m.POCorrespondence.LetterNo))
                        {
                            if (!string.IsNullOrEmpty(m.POCorrespondence.Description))
                            {
                                if (!string.IsNullOrEmpty(m.POCorrespondence.Subject))
                                {
                                    if (!string.IsNullOrEmpty(m.POCorrespondence.TypeOfCorrespondence))
                                    {
                                        if (m.POCorrespondence.TypeOfCorrespondence == "IN")
                                        {
                                            if (!string.IsNullOrEmpty(m.POCorrespondence.From))
                                            {
                                                m.POCorrespondence.To = string.Empty;
                                            }
                                            else
                                            {
                                                TempData["Error"] = "Please enter From.";
                                                return View(m);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(m.POCorrespondence.To))
                                            {
                                                m.POCorrespondence.From = string.Empty;
                                            }
                                            else
                                            {
                                                TempData["Error"] = "Please enter To.";
                                                return View(m);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(m.POCorrespondence.CopyTo))
                                        {
                                            if (inputFile != null && inputFile.ContentLength > 0)
                                            {
                                                if (inputFile.ContentType == "application/pdf")
                                                {
                                                    Guid FileName = Guid.NewGuid();
                                                    m.POCorrespondence.FilePath = "/Uploads/POCorrespondence/" + FileName + ".pdf";
                                                    string tPath = Path.Combine(Server.MapPath("~/Uploads/POCorrespondence/"), FileName + ".pdf");
                                                    inputFile.SaveAs(tPath);
                                                }
                                                else
                                                {
                                                    TempData["Error"] = "Only PDF Files.";
                                                    return View(m);
                                                }
                                            }
                                            else
                                            {
                                                m.POCorrespondence.FilePath = m.file;
                                            }

                                            m.POCorrespondence.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                            m.POCorrespondence.CreatedDate = DateTime.Now;

                                            using (var ctx = new INDMSEntities())
                                            {
                                                ctx.Entry(m.POCorrespondence).State = EntityState.Modified;

                                                ctx.SaveChanges();
                                            }

                                            TempData["RowId"] = m.POCorrespondence.Id;
                                            TempData["MSG"] = "Save Successfully";

                                            return RedirectToAction("Index", "POFlow", new { id = m.POCorrespondence.POId });
                                        }
                                        else
                                        {
                                            TempData["Error"] = "Please Enter Copy To.";
                                        }
                                    }
                                    else
                                    {
                                        TempData["Error"] = "Please select type of Correspondence.";
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = "Please enter Subject";
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Please enter Description";
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please enter Letter No";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Please enter File No";
                    }
                }
                else
                {
                    TempData["Error"] = "Please enter PO No";
                }
            }
            else
            {
                TempData["Error"] = "PO is not assign.";
            }
            return View(m);
        }
    }
}