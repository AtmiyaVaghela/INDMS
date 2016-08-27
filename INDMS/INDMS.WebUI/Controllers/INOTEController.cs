using INDMS.WebUI.Infrastructure.Filters;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers
{
    public class INOTEController : Controller
    {
        public ActionResult New(int? Id)
        {
            INOTEViewModel m = new INOTEViewModel();

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

                    m.CoveringLetters = ctx.CoveringLetters.Where(x => x.POId == Id);

                    decimal total = ctx.CoveringLetters.Where(x => x.POId == Id).Sum(x => x.POValue) ?? 0;
                    if (m.PO != null)
                    {
                        if (m.PO.DeliveryDate >= DateTime.Now)
                        {
                            if (m.PO.POValue > total)
                            {
                                m.CoveringLetter = new CoveringLetter();
                                m.CoveringLetter.POId = m.PO.Id;
                                m.CoveringLetter.PONo = m.PO.PONo;
                            }
                            else
                            {
                                TempData["Error"] = "PO value has reached the limit.";
                                return RedirectToAction("Index", "POFlow", new { Id = Id });
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Delivery date Is Passed.";
                            return RedirectToAction("Index", "POFlow", new { Id = Id });
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

        [HttpPost]
        [AuthUser]
        [ValidateAntiForgeryToken]
        public ActionResult New(INOTEViewModel m, HttpPostedFileBase inputFile)
        {
            if ((m.CoveringLetter.POId ?? 0) != 0)
            {
                if (!string.IsNullOrEmpty(m.CoveringLetter.PONo))
                {
                    if (!string.IsNullOrEmpty(m.CoveringLetter.FileNo))
                    {
                        if (!string.IsNullOrEmpty(m.CoveringLetter.NoOfLots))
                        {
                            if ((m.CoveringLetter.POValue ?? 0) > 0)
                            {
                                if (!string.IsNullOrEmpty(m.CoveringLetter.Warantee))
                                {
                                    using (var db = new INDMSEntities())
                                    {
                                        try
                                        {
                                            db.CoveringLetters.Add(m.CoveringLetter);
                                            db.SaveChanges();

                                            return RedirectToAction("Index", "POFlow", new { Id = m.CoveringLetter.POId });
                                        }
                                        catch (Exception ex)
                                        {
                                            TempData["Error"] = ex.Message.ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    TempData["Error"] = "Please Enter Warantee / Gurantee.";
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Please Enter PO Value.";
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please Enter No Of Lots";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Please Enter File No.";
                    }
                }
                else
                {
                    TempData["Error"] = "Please Enter PO No.";
                }
            }
            else
            {
                TempData["Error"] = "PO is not assign.";
            }

            return View(m);
        }

        public ActionResult Edit(int? Id)
        {
            INOTEViewModel m = new INOTEViewModel();

            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.CoveringLetter = ctx.CoveringLetters.Find(Id);
                    if (m.CoveringLetter != null)
                    {
                        m.POGeneration = ctx.POGenerations.Where(x => x.PO_ID == m.CoveringLetter.POId).SingleOrDefault();

                        m.PO = ctx.PurchaseOrders.FirstOrDefault(x => x.Id == m.CoveringLetter.POId);

                        if (m.PO != null)
                        {
                            if (m.PO.DeliveryDate >= DateTime.Now)
                            {
                                return View(m);
                            }
                            else
                            {
                                TempData["Error"] = "Delivery date Is Passed.";
                                return RedirectToAction("Index", "POFlow", new { Id = m.PO.Id });
                            }
                        }
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                }
            }
            return View();
        }

        public ActionResult Print(int? Id)
        {
            INOTEViewModel m = new INOTEViewModel();
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                using (var ctx = new INDMSEntities())
                {
                    m.CoveringLetter = ctx.CoveringLetters.Find(Id);
                    if (m.CoveringLetter != null)
                    {
                        m.PO = ctx.PurchaseOrders.SingleOrDefault(x => x.Id == m.CoveringLetter.POId);
                        Int32 z = Convert.ToInt32(m.PO.Firm ?? "0");
                        Firm firm = ctx.Firms.Find(z);
                        m.PO.Firm = firm.FirmName;
                        m.PO.FirmAddress = firm.FirmAddress;
                    }
                }
            }

            return View(m);
        }
    }
}