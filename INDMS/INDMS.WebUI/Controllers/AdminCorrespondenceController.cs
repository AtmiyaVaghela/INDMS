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
    public class AdminCorrespondenceController : Controller
    {
        // GET: AdminCorrespondence
        [AuthUser]
        public ActionResult Index()
        {
            AdminCorrespondenceViewModel m = new AdminCorrespondenceViewModel();
            using (var ctx = new INDMSEntities())
            {
                m.AdminCorrespondences = ctx.AdminCorrespondences.ToList();
            }

            return View(m);
        }

        [AuthUser]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [AuthUser]
        [ValidateAntiForgeryToken]
        public ActionResult New(AdminCorrespondenceViewModel m, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(m.AdminCorrespondence.FileNo))
                {
                    if (!string.IsNullOrEmpty(m.AdminCorrespondence.LetterNo))
                    {
                        if (!string.IsNullOrEmpty(m.AdminCorrespondence.Subject))
                        {
                            if (!string.IsNullOrEmpty(m.AdminCorrespondence.TypeOf))
                            {
                                if (!string.IsNullOrEmpty(m.AdminCorrespondence.From))
                                {
                                    if (!string.IsNullOrEmpty(m.AdminCorrespondence.To))
                                    {
                                        if (!string.IsNullOrEmpty(m.AdminCorrespondence.CopyTo))
                                        {
                                            if (inputFile != null && inputFile.ContentLength > 0)
                                            {
                                                if (inputFile.ContentType == "application/pdf")
                                                {
                                                    Guid FileName = Guid.NewGuid();
                                                    m.AdminCorrespondence.FilePath = "/Uploads/AdminCorrespondence/" + FileName + ".pdf";
                                                    string tPath = Path.Combine(Server.MapPath("~/Uploads/AdminCorrespondence/"), FileName + ".pdf");
                                                    inputFile.SaveAs(tPath);

                                                    m.AdminCorrespondence.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                                    m.AdminCorrespondence.CreatedDate = null;

                                                    using (var ctx = new INDMSEntities())
                                                    {
                                                        ctx.AdminCorrespondences.Add(m.AdminCorrespondence);
                                                        ctx.SaveChanges();
                                                    }

                                                    TempData["RowId"] = m.AdminCorrespondence.Id;
                                                    TempData["MSG"] = "Save Successfully";

                                                    return RedirectToAction("Index");
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
                                            ModelState.AddModelError("AdminCorrespondence.To", "Please Enter Copy To");
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("AdminCorrespondence.To", "Please Enter To");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("AdminCorrespondence.From", "Please Enter From");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("AdminCorrespondence.TypeOf", "Please Enter Type of Correspondence");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("AdminCorrespondence.Subject", "Please Enter Subject");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("AdminCorrespondence.LetterNo", "Please Enter Letter No");
                    }
                }
                else
                {
                    ModelState.AddModelError("AdminCorrespondence.FileNo", "Please Enter File no.");
                }
            }
            return View(m);
        }

        [AuthUser]
        public ActionResult Edit(int? Id)
        {
            AdminCorrespondenceViewModel m = new AdminCorrespondenceViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.AdminCorrespondence = ctx.AdminCorrespondences.Find(Id);
                }

                if (m.AdminCorrespondence == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    m.file = m.AdminCorrespondence.FilePath;
                }
            }
            return View(m);
        }

        [HttpPost]
        [AuthUser]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminCorrespondenceViewModel m, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(m.AdminCorrespondence.FileNo))
                {
                    if (!string.IsNullOrEmpty(m.AdminCorrespondence.LetterNo))
                    {
                        if (!string.IsNullOrEmpty(m.AdminCorrespondence.Subject))
                        {
                            if (!string.IsNullOrEmpty(m.AdminCorrespondence.TypeOf))
                            {
                                if (!string.IsNullOrEmpty(m.AdminCorrespondence.From))
                                {
                                    if (!string.IsNullOrEmpty(m.AdminCorrespondence.To))
                                    {
                                        if (!string.IsNullOrEmpty(m.AdminCorrespondence.CopyTo))
                                        {
                                            if (inputFile != null && inputFile.ContentLength > 0)
                                            {
                                                if (inputFile.ContentType == "application/pdf")
                                                {
                                                    Guid FileName = Guid.NewGuid();
                                                    m.AdminCorrespondence.FilePath = "/Uploads/AdminCorrespondence/" + FileName + ".pdf";
                                                    string tPath = Path.Combine(Server.MapPath("~/Uploads/AdminCorrespondence/"), FileName + ".pdf");
                                                    inputFile.SaveAs(tPath);
                                                }
                                                else
                                                {
                                                    TempData["Error"] = "Only PDF Files.";
                                                }
                                            }
                                            else
                                            {
                                                m.AdminCorrespondence.FilePath = m.file;
                                            }

                                            m.AdminCorrespondence.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                            m.AdminCorrespondence.CreatedDate = DateTime.Now;

                                            using (var ctx = new INDMSEntities())
                                            {
                                                try
                                                {
                                                    ctx.Entry(m.AdminCorrespondence).State = EntityState.Modified;
                                                    ctx.SaveChanges();
                                                    ModelState.Clear();
                                                }
                                                catch (Exception ex)
                                                {
                                                    throw ex;
                                                }
                                            }

                                            TempData["RowId"] = m.AdminCorrespondence.Id;
                                            TempData["MSG"] = "Save Successfully";

                                            return RedirectToAction("Index");
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("AdminCorrespondence.To", "Please Enter Copy To");
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("AdminCorrespondence.To", "Please Enter To");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("AdminCorrespondence.From", "Please Enter From");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("AdminCorrespondence.TypeOf", "Please Enter Type of Correspondence");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("AdminCorrespondence.Subject", "Please Enter Subject");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("AdminCorrespondence.LetterNo", "Please Enter Letter No");
                    }
                }
                else
                {
                    ModelState.AddModelError("AdminCorrespondence.FileNo", "Please Enter File no.");
                }
            }
            return View(m);
        }
    }
}