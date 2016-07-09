﻿using INDMS.WebUI.Infrastructure;
using INDMS.WebUI.Infrastructure.Filters;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace INDMS.WebUI.Controllers {
    public class PurchaseOrderController : Controller {
        // GET: PurchaseOrder
        [AuthUser]
        public ActionResult Index() {
            PurchaseOrderViewModel m = new PurchaseOrderViewModel();
            using (var ctx = new INDMSEntities()) {
                m.PurchaseOrders = ctx.PurchaseOrders.Where(x => x.Flag != "CLOSED").ToList();

                foreach (var item in m.PurchaseOrders) {
                    string[] Inspectors = item.Inspectors.Split(',');
                    item.Inspectors = string.Empty;
                    foreach (var ins in Inspectors) {
                        item.Inspectors += ctx.Users.SingleOrDefault(z => z.UserId == new Guid(ins)).Name + ",";
                    }

                    item.Inspectors.Substring(0, item.Inspectors.Length - 1);

                    item.Firm = ctx.Firms.SingleOrDefault(z => z.Id.ToString() == item.Firm).FirmName;
                }
            }
            
            return View(m);
        }

        [AuthUser]
        public ActionResult New() {
            return View();
        }

        [HttpPost]
        [AuthUser]
        public ActionResult New(PurchaseOrderViewModel m, HttpPostedFileBase inputFile) {
            if (!string.IsNullOrEmpty(m.PurchaseOrder.FileNo)) {

                if (!string.IsNullOrEmpty(m.PurchaseOrder.PONo)) {

                    if (!string.IsNullOrEmpty(Convert.ToString(m.PurchaseOrder.POValue))) {

                        if (m.PurchaseOrder.Quantity > (decimal)0.00) {

                            if (m.PurchaseOrder.NoOfLots > (decimal)0.00) {

                                if (!string.IsNullOrEmpty(m.PurchaseOrder.PoPlacingAuthority)) {

                                    if (m.PurchaseOrder.PoPlacingAuthority.Equals("OTHERS")) {

                                        if (!string.IsNullOrEmpty(m.OPOPlacingAuthority)) {

                                            m.PurchaseOrder.PoPlacingAuthority = m.OPOPlacingAuthority;
                                            AddParam("POPlacingAuthority", m.OPOPlacingAuthority.Trim());

                                        }
                                        else {
                                            TempData["Error"] = "Please enter PO Placing Authority.";
                                        }
                                    }

                                    if (m.InspectorId.Length > 0) {

                                        foreach (var i in m.InspectorId) {
                                            m.PurchaseOrder.Inspectors += i + ",";
                                        }

                                        m.PurchaseOrder.Inspectors = m.PurchaseOrder.Inspectors.Substring(0, m.PurchaseOrder.Inspectors.Length - 1);

                                        if (!string.IsNullOrEmpty(m.PurchaseOrder.Firm)) {

                                            m.PurchaseOrder.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                            m.PurchaseOrder.CreatedDate = DateTime.Now;

                                            if (inputFile != null && inputFile.ContentLength > 0) {
                                                if (inputFile.ContentType == "application/pdf") {

                                                    Guid FileName = Guid.NewGuid();
                                                    m.PurchaseOrder.FilePath = "/Uploads/PurchaseOrder/" + FileName + ".pdf";
                                                    string tPath = Path.Combine(Server.MapPath("~/Uploads/PurchaseOrder/"), FileName + ".pdf");
                                                    inputFile.SaveAs(tPath);

                                                    using (var ctx = new INDMSEntities()) {
                                                        ctx.PurchaseOrders.Add(m.PurchaseOrder);
                                                        ctx.SaveChanges();
                                                    }

                                                    TempData["RowId"] = m.PurchaseOrder.Id;
                                                    TempData["MSG"] = "Save Successfully";

                                                    return RedirectToAction("Index");

                                                }
                                                else {
                                                    TempData["Error"] = "Please select Only PDF File";
                                                }
                                            }
                                            else {
                                                TempData["Error"] = "Please select file.";
                                            }
                                        }
                                        else {
                                            TempData["Error"] = "Please select Firm.";
                                        }
                                    }
                                    else {
                                        TempData["Error"] = "Please select Inspector";
                                    }
                                }
                                else {
                                    TempData["Error"] = "Please select PO Placing Authority.";
                                }
                            }
                            else {
                                TempData["Error"] = "No. Of Lots must be greater than 0.00";
                            }
                        }
                        else {
                            TempData["Error"] = "PO Value must be greater than 0.00";
                        }
                    }
                    else {
                        TempData["Error"] = "Please enter PO Value.";
                    }
                }
                else {
                    TempData["Error"] = "Please enter PO No.";
                }
            }
            else {
                TempData["Error"] = "Please enter file No.";
            }
            return View(m);
        }

        private void AddParam(string strKeyName, string strKeyValue) {
            AddNewParameter obj = new AddNewParameter();
            string keyName = strKeyName;
            string keyValue = strKeyValue;
            obj.Add(keyName, keyValue);
        }

    }
}