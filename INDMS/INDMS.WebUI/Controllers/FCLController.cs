using CrystalDecisions.CrystalReports.Engine;
using INDMS.WebUI.Infrastructure;
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

                    List<FCLDetail> fclDetails = new List<FCLDetail>();
                    fclDetails.Add(new FCLDetail { POSrNo = "", PODetails = "" });
                    //fclDetails.Add(new FCLDetail { POSrNo = "3", PODetails = "4" });

                    m.FCLDetails = fclDetails;
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
                    m.FCL.POId = m.PO.Id;
                    m.FCL.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                    m.FCL.CreatedDate = null;

                    using (var ctx = new INDMSEntities())
                    {
                        using (var ctxTr = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                ctx.FCLs.Add(m.FCL);
                                ctx.SaveChanges();

                                foreach (var item in m.FCLDetails)
                                {
                                    item.FCLId = m.FCL.Id;
                                    ctx.FCLDetails.Add(item);
                                    ctx.SaveChanges();
                                }

                                ctxTr.Commit();
                            }
                            catch (Exception)
                            {
                                ctxTr.Rollback();
                            }
                        }
                    }

                    TempData["RowId"] = m.FCL.Id;
                    TempData["MSG"] = "Save Successfully";

                    return RedirectToAction("Index", "POFlow", new { id = m.PO.Id });
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
                        m.FCLDetails = (from d in ctx.FCLDetails
                                        where d.FCLId == Id
                                        select d).ToList();
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
                    m.FCL.POId = m.PO.Id;
                    m.FCL.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                    m.FCL.CreatedDate = DateTime.Now;

                    using (var ctx = new INDMSEntities())
                    {
                        using (var ctxTr = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                ctx.Entry(m.FCL).State = EntityState.Modified;
                                ctx.SaveChanges();

                                List<FCLDetail> fclDetails = (from d in ctx.FCLDetails
                                                              where d.FCLId == m.FCL.Id
                                                              select d).ToList();

                                foreach (var item in fclDetails)
                                {
                                    ctx.Entry(item).State = EntityState.Deleted;
                                    ctx.SaveChanges();
                                }

                                foreach (var item in m.FCLDetails)
                                {
                                    if (!string.IsNullOrEmpty(item.POSrNo) && !string.IsNullOrEmpty(item.PODetails))
                                    {
                                        item.FCLId = m.FCL.Id;
                                        ctx.FCLDetails.Add(item);
                                        ctx.SaveChanges();
                                    }
                                }

                                ctxTr.Commit();
                            }
                            catch (Exception)
                            {
                                ctxTr.Rollback();
                            }
                        }
                    }

                    TempData["RowId"] = m.FCL.Id;
                    TempData["MSG"] = "Save Successfully";

                    return RedirectToAction("Index", "POFlow", new { id = m.PO.Id });
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
            List<AppendixADetailsViewModel> mddetails = new List<AppendixADetailsViewModel>();

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
                    string reportName = "AppendixAI.rpt";
                    if (m.FCL.Flag.Equals("ACCEPTED"))
                    {
                        reportName = "AppendixA.rpt";
                    }

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

                            mdl.Add(md);

                            var fclDetails = (from d in ctx.FCLDetails
                                              where d.FCLId == Id
                                              select d).ToList();

                            foreach (var item in fclDetails)
                            {
                                mddetails.Add(new AppendixADetailsViewModel { PoSrNo = item.POSrNo, PoDetails = item.PODetails });
                            }

                            ReportDocument rd = new ReportDocument();
                            rd.Load(Path.Combine(Server.MapPath("~/Reports"), reportName));
                            rd.Database.Tables[0].SetDataSource(mdl);
                            rd.Database.Tables[1].SetDataSource(mddetails);

                            Response.Buffer = false;
                            Response.ClearContent();
                            Response.ClearHeaders();

                            try
                            {
                                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                                stream.Seek(0, SeekOrigin.Begin);
                                return File(stream, "application/pdf", reportName.Replace("rpt","pdf"));
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

        [CAuthRole("Admin")]
        public ActionResult Approve()
        {
            FCLViewModel m = new FCLViewModel();

            using (var ctx = new INDMSEntities())
            {
                m.FCLs = ctx.FCLs.Where(x => x.Flag == "OPEN" || x.Flag == null).ToList();
                //m.PO = ctx.PurchaseOrders.FirstOrDefault(x => x.Id == m.FCL.POId);
            }

            return View(m);
        }

        [HttpPost]
        [AuthUser]
        [CAuthRole("Admin")]
        public ActionResult Approve(int? id)
        {
            string Msg = string.Empty;
            FCLViewModel m = new FCLViewModel();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.FCL = ctx.FCLs.Find(id);
                }

                if (m.FCL == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    m.FCL.Flag = "ACCEPTED";
                    using (var ctx = new INDMSEntities())
                    {
                        ctx.Entry(m.FCL).State = System.Data.Entity.EntityState.Modified;
                        ctx.SaveChanges();
                        Msg = "success";

                        var poFlow = ctx.POGenerations.Where(x => x.PO_ID == m.FCL.POId).SingleOrDefault();
                        POFlowViewModel mx = new POFlowViewModel();
                        if (poFlow != null)
                        {
                            mx.POGeneration = new POGeneration();
                            mx.POGeneration = poFlow;
                            mx.POGeneration.PO_ID = m.FCL.POId;
                            mx.POGeneration.FCL = 1;
                            mx.POGeneration.PO_CORRESPONDENCE = 1;
                            mx.POGeneration.DRAWING = 1;
                            mx.POGeneration.QAP = 1;

                            ctx.Entry(mx.POGeneration).State = EntityState.Modified;
                            ctx.SaveChanges();
                            Msg = "Success";
                        }
                        else
                        {
                            mx.POGeneration = new POGeneration();
                            mx.POGeneration.PO_ID = m.FCL.POId;
                            mx.POGeneration.FCL = 1;
                            mx.POGeneration.PO_CORRESPONDENCE = 1;
                            mx.POGeneration.DRAWING = 1;
                            mx.POGeneration.QAP = 1;

                            ctx.POGenerations.Add(mx.POGeneration);
                            ctx.SaveChanges();
                            Msg = "Success";
                        }
                    }
                }
            }

            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthUser]
        [CAuthRole("Admin")]
        public ActionResult Reject(int? id)
        {
            string Msg = string.Empty;
            FCLViewModel m = new FCLViewModel();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.FCL = ctx.FCLs.Find(id);
                }

                if (m.FCL == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    m.FCL.Flag = "REJECTED";
                    using (var ctx = new INDMSEntities())
                    {
                        ctx.Entry(m.FCL).State = System.Data.Entity.EntityState.Modified;
                        ctx.SaveChanges();
                        Msg = "success";
                    }
                }
            }

            return Json(Msg, JsonRequestBehavior.AllowGet);
        }
    }
}