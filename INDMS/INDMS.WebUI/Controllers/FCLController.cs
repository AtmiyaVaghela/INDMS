using CrystalDecisions.CrystalReports.Engine;
using INDMS.WebUI.Infrastructure.Filters;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers
{
    public class FCLController : Controller
    {
        // GET: FCL
        public ActionResult Index()
        {
            return View();
        }

        [AuthUser]
        public ActionResult New(int? Id)
        {
            FCLViewModel m = new FCLViewModel();

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
                        m.FCL = new FCL();
                        m.FCL.POId = Id ?? 0;
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
        public ActionResult New(FCLViewModel m)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(m.PO.Id)))
            {
                if (!string.IsNullOrEmpty(m.FCL.POName))
                {
                    if (!string.IsNullOrEmpty(m.FCL.POSrNo))
                    {
                        if (!string.IsNullOrEmpty(m.FCL.Details))
                        {
                            m.FCL.POId = m.PO.Id;
                            m.FCL.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                            m.FCL.CreatedDate = null;

                            using (var ctx = new INDMSEntities())
                            {
                                ctx.FCLs.Add(m.FCL);
                                ctx.SaveChanges();
                            }

                            POGeneration POG = new POGeneration();
                            using (var ctx = new INDMSEntities())
                            {
                                POG = ctx.POGenerations.SingleOrDefault(x => x.PO_ID == m.PO.Id);
                                POG.PO_CORRESPONDENCE = 1;
                                POG.DRAWING = 1;
                                POG.QAP = 1;

                                ctx.Entry(POG).State = EntityState.Modified;
                                ctx.SaveChanges();
                                ModelState.Clear();
                            }

                            TempData["RowId"] = m.FCL.Id;
                            TempData["MSG"] = "Save Successfully";

                            return RedirectToAction("Index", "POFlow", new { id = m.PO.Id });
                        }
                        else
                        {
                            TempData["Error"] = "Please enter Details.";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Please enter Sr. No.";
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
            FCLViewModel m = new FCLViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.FCL = ctx.FCLs.Find(Id);
                    if (m.FCL != null)
                    {
                        m.POGeneration = ctx.POGenerations.Where(x => x.PO_ID == m.FCL.POId).SingleOrDefault();
                        m.PO = ctx.PurchaseOrders.Find(m.FCL.POId);
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
        public ActionResult Edit(FCLViewModel m)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(m.PO.Id)))
            {
                if (!string.IsNullOrEmpty(m.FCL.POName))
                {
                    if (!string.IsNullOrEmpty(m.FCL.POSrNo))
                    {
                        if (!string.IsNullOrEmpty(m.FCL.Details))
                        {
                            m.FCL.POId = m.PO.Id;
                            m.FCL.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                            m.FCL.CreatedDate = DateTime.Now;

                            using (var ctx = new INDMSEntities())
                            {
                                ctx.Entry(m.FCL).State = EntityState.Modified;
                                ctx.SaveChanges();
                                ModelState.Clear();
                            }

                            TempData["RowId"] = m.FCL.Id;
                            TempData["MSG"] = "Save Successfully";

                            return RedirectToAction("Index", "POFlow", new { id = m.PO.Id });
                        }
                        else
                        {
                            TempData["Error"] = "Please enter Details.";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Please enter Sr. No.";
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
        public ActionResult Print(int? Id)
        {
            FCLViewModel m = new FCLViewModel();
            List<AppendixAViewModel> mdl = new List<AppendixAViewModel>();

            AppendixAViewModel md = new AppendixAViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.FCL = ctx.FCLs.Find(Id);
                    if (m.FCL == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    else
                    {
                        m.POGeneration = ctx.POGenerations.Where(x => x.PO_ID == m.FCL.POId).SingleOrDefault();
                        m.PO = ctx.PurchaseOrders.Find(m.FCL.POId);
                        if (m.PO != null)
                        {
                            md.FCLNo = m.FCL.Id.ToString();
                            md.Date = DateTime.Now.ToString("dd-MM-yyyy");
                            int firmId = Convert.ToInt32(m.PO.Firm);
                            m.PO.Firm = ctx.Firms.SingleOrDefault(z => z.Id.ToString() == m.PO.Firm).FirmName;
                            m.PO.FirmAddress = ctx.Firms.SingleOrDefault(z => z.Id == firmId).FirmAddress;

                            md.FirmName = m.PO.Firm;
                            md.FirmAddress = m.PO.FirmAddress;

                            md.PONo = m.PO.PONo;
                            md.PODate = m.PO.PODate.ToString();
                            md.Subject = string.Empty;
                            md.POPlacingAuthority = m.PO.PoPlacingAuthority;
                            md.DeliveryDate = m.PO.DeliveryDate == null ? string.Empty : Convert.ToDateTime(Convert.ToString(m.PO.DeliveryDate)).ToString("dd-MM-yyyy");

                            string[] Inspectors = m.PO.Inspectors.Split(',');
                            m.PO.Inspectors = string.Empty;
                            foreach (var ins in Inspectors)
                            {
                                m.PO.Inspectors += ctx.Users.SingleOrDefault(z => z.UserId == new Guid(ins)).Name + ",";
                            }

                            md.InspectorDetails = m.PO.Inspectors.Substring(0, m.PO.Inspectors.Length - 1);

                            md.POSrNo = m.FCL.POSrNo;
                            md.Details = m.FCL.Details;

                            mdl.Add(md);

                            ReportDocument rd = new ReportDocument();
                            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "AppendixA.rpt"));
                            rd.SetDataSource(mdl);

                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();

                            try
                            {
                                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                                stream.Seek(0, SeekOrigin.Begin);
                                return File(stream, "application/pdf", "AppendixA.pdf");
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
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