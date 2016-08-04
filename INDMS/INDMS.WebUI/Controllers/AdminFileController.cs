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
    public class AdminFileController : Controller
    {
        // GET: AdminFile
        [AuthUser]
        public ActionResult Index()
        {
            AdminFileViewModel m = new AdminFileViewModel();
            using (var ctx = new INDMSEntities())
            {
                m.Files = ctx.Files.ToList();
            }

            return View(m);
        }

        [AuthUser]
        public ActionResult New()
        {
            return View();
        }

        [AuthUser]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult New(AdminFileViewModel m, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(m.File.FileNo))
                {
                    if (!string.IsNullOrEmpty(m.File.Subject))
                    {
                        if (inputFile != null && inputFile.ContentLength > 0)
                        {
                            if (inputFile.ContentType == "application/pdf")
                            {
                                Guid FileName = Guid.NewGuid();
                                m.File.FilePath = "/Uploads/AdminFile/" + FileName + ".pdf";
                                string tPath = Path.Combine(Server.MapPath("~/Uploads/AdminFile/"), FileName + ".pdf");
                                inputFile.SaveAs(tPath);

                                m.File.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                m.File.CreatedDate = null;

                                using (var ctx = new INDMSEntities())
                                {
                                    ctx.Files.Add(m.File);
                                    ctx.SaveChanges();
                                }

                                TempData["RowId"] = m.File.Id;
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
                        TempData["Error"] = "Please enter subject.";
                    }
                }
                else
                {
                    TempData["Error"] = "Please enter file no.";
                }
            }
            return View();
        }

        [AuthUser]
        public ActionResult Edit(int? Id)
        {
            AdminFileViewModel m = new AdminFileViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.File = ctx.Files.Find(Id);
                }

                if (m.File == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    m.file = m.File.FilePath;
                }
            }
            return View(m);
        }

        [AuthUser]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(AdminFileViewModel m, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(m.File.FileNo))
                {
                    if (!string.IsNullOrEmpty(m.File.Subject))
                    {
                        if (inputFile != null && inputFile.ContentLength > 0)
                        {
                            if (inputFile.ContentType == "application/pdf")
                            {
                                Guid FileName = Guid.NewGuid();
                                m.File.FilePath = "/Uploads/AdminFile/" + FileName + ".pdf";
                                string tPath = Path.Combine(Server.MapPath("~/Uploads/AdminFile/"), FileName + ".pdf");
                                inputFile.SaveAs(tPath);
                            }
                            else
                            {
                                TempData["Error"] = "Only PDF Files.";
                            }
                        }
                        else
                        {
                            m.File.FilePath = m.file;
                        }

                        m.File.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                        m.File.CreatedDate = null;

                        using (var ctx = new INDMSEntities())
                        {
                            try
                            {
                                ctx.Entry(m.File).State = EntityState.Modified;
                                ctx.SaveChanges();
                                ModelState.Clear();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }

                        TempData["RowId"] = m.File.Id;
                        TempData["MSG"] = "Save Successfully";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = "Please enter subject.";
                    }
                }
                else
                {
                    TempData["Error"] = "Please enter file no.";
                }
            }
            return View();
        }
    }
}