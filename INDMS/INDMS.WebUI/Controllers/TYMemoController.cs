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
    public class TYMemoController : Controller
    {
        // GET: TYMemo
        [AuthUser]
        public ActionResult Index()
        {
            TYMemoViewModel m = new TYMemoViewModel();
            using (var ctx = new INDMSEntities())
            {
                m.TYMemos = ctx.TYMemoes.ToList();
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
        public ActionResult New(TYMemoViewModel m, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(m.TYMemo.FileNo))
                {
                    if (!string.IsNullOrEmpty(m.TYMemo.TYMemoNO))
                    {
                        if (!string.IsNullOrEmpty(m.TYMemo.Subject))
                        {
                            if (inputFile != null && inputFile.ContentLength > 0)
                            {
                                if (inputFile.ContentType == "application/pdf")
                                {
                                    Guid FileName = Guid.NewGuid();
                                    m.TYMemo.FilePath = "/Uploads/TYMemos/" + FileName + ".pdf";
                                    string tPath = Path.Combine(Server.MapPath("~/Uploads/TYMemos/"), FileName + ".pdf");
                                    inputFile.SaveAs(tPath);

                                    m.TYMemo.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                    m.TYMemo.CreatedDate = DateTime.Now;

                                    using (var ctx = new INDMSEntities())
                                    {
                                        ctx.TYMemoes.Add(m.TYMemo);
                                        ctx.SaveChanges();
                                    }

                                    TempData["RowId"] = m.TYMemo.Id;
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
                            ModelState.AddModelError("TYMemo.Subject", "Please Enter Subject");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("TYMemo.TYMemoNO", "Please Enter TY Memo no.");
                    }
                }
                else
                {
                    ModelState.AddModelError("TYMemo.FileNo", "Please Enter File no.");
                }
            }
            return View(m);
        }

        [AuthUser]
        public ActionResult Edit(int? Id)
        {
            TYMemoViewModel m = new TYMemoViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.TYMemo = ctx.TYMemoes.Find(Id);
                }

                if (m.TYMemo == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    m.File = m.TYMemo.FilePath;
                }
            }
            return View(m);
        }

        [HttpPost]
        [AuthUser]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TYMemoViewModel m, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(m.TYMemo.FileNo))
                {
                    if (!string.IsNullOrEmpty(m.TYMemo.TYMemoNO))
                    {
                        if (!string.IsNullOrEmpty(m.TYMemo.Subject))
                        {
                            if (inputFile != null && inputFile.ContentLength > 0)
                            {
                                if (inputFile.ContentType == "application/pdf")
                                {
                                    Guid FileName = Guid.NewGuid();
                                    m.TYMemo.FilePath = "/Uploads/TYMemos/" + FileName + ".pdf";
                                    string tPath = Path.Combine(Server.MapPath("~/Uploads/TYMemos/"), FileName + ".pdf");
                                    inputFile.SaveAs(tPath);
                                }
                                else
                                {
                                    TempData["Error"] = "Only PDF Files.";
                                }
                            }
                            else
                            {
                                m.TYMemo.FilePath = m.File;
                            }

                            m.TYMemo.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                            m.TYMemo.CreatedDate = DateTime.Now;

                            using (var ctx = new INDMSEntities())
                            {
                                try
                                {
                                    ctx.Entry(m.TYMemo).State = EntityState.Modified;
                                    ctx.SaveChanges();
                                    ModelState.Clear();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }

                            TempData["RowId"] = m.TYMemo.Id;
                            TempData["MSG"] = "Save Successfully";

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("TYMemo.Subject", "Please Enter Subject");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("TYMemo.TYMemoNO", "Please Enter TY Memo no.");
                    }
                }
                else
                {
                    ModelState.AddModelError("TYMemo.FileNo", "Please Enter File no.");
                }
            }
            return View(m);
        }
    }
}