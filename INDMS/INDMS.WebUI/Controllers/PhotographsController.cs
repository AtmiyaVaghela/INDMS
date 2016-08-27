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
    public class PhotographsController : Controller
    {
        // GET: Photographs
        public ActionResult Index()
        {
            PhotographsViewModel m = new PhotographsViewModel();
            using (var ctx = new INDMSEntities())
            {
                m.Photographs = ctx.Photographs.ToList();
            }

            return View(m);
        }

        [AuthUser]
        public ActionResult New(int? Id)
        {
            PhotographsViewModel m = new PhotographsViewModel();

            if (Id == null)
            {
                return View();
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.PO = ctx.PurchaseOrders.Find(Id);
                    if (m.PO != null)
                    {
                        m.Photograph = new Photograph();
                        m.Photograph.POId = m.PO.Id;
                        m.Photograph.PONo = m.PO.PONo;
                    }
                }

                return View(m);
            }
        }

        [AuthUser]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult New(PhotographsViewModel m, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(m.Photograph.FolderName))
                {
                    if (!string.IsNullOrEmpty(m.Photograph.Subject))
                    {
                        if (inputFile != null && inputFile.ContentLength > 0)
                        {
                            if (inputFile.ContentType == "application/pdf")
                            {
                                Guid FileName = Guid.NewGuid();
                                m.Photograph.FilePath = "/Uploads/Photographs/" + FileName + ".pdf";
                                string tPath = Path.Combine(Server.MapPath("~/Uploads/Photographs/"), FileName + ".pdf");
                                inputFile.SaveAs(tPath);

                                m.Photograph.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                m.Photograph.CreatedDate = null;

                                using (var ctx = new INDMSEntities())
                                {
                                    ctx.Photographs.Add(m.Photograph);
                                    ctx.SaveChanges();
                                }

                                TempData["RowId"] = m.Photograph.Id;
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
                        ModelState.AddModelError("Photograph.FolderName", "Please Enter Subject");
                    }
                }
                else
                {
                    ModelState.AddModelError("Photograph.FileNo", "Please Enter folder name");
                }
            }
            return View();
        }

        [AuthUser]
        public ActionResult Edit(int? Id)
        {
            PhotographsViewModel m = new PhotographsViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.Photograph = ctx.Photographs.Find(Id);
                }

                if (m.Photograph == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    m.File = m.Photograph.FilePath;
                }
            }
            return View(m);
        }

        [HttpPost]
        [AuthUser]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhotographsViewModel m, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(m.Photograph.FolderName))
                {
                    if (!string.IsNullOrEmpty(m.Photograph.Subject))
                    {
                        if (inputFile != null && inputFile.ContentLength > 0)
                        {
                            if (inputFile.ContentType == "application/pdf")
                            {
                                Guid FileName = Guid.NewGuid();
                                m.Photograph.FilePath = "/Uploads/Photographs/" + FileName + ".pdf";
                                string tPath = Path.Combine(Server.MapPath("~/Uploads/Photographs/"), FileName + ".pdf");
                                inputFile.SaveAs(tPath);
                            }
                            else
                            {
                                TempData["Error"] = "Only PDF Files.";
                            }
                        }
                        else
                        {
                            m.Photograph.FilePath = m.File;
                        }

                        m.Photograph.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                        m.Photograph.CreatedDate = DateTime.Now;

                        using (var ctx = new INDMSEntities())
                        {
                            try
                            {
                                ctx.Entry(m.Photograph).State = EntityState.Modified;
                                ctx.SaveChanges();
                                ModelState.Clear();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }

                        TempData["RowId"] = m.Photograph.Id;
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
                    TempData["Error"] = "Please enter Folder Name";
                }
            }
            return View(m);
        }
    }
}