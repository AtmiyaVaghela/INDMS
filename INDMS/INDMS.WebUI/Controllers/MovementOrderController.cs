using CrystalDecisions.CrystalReports.Engine;
using INDMS.WebUI.Infrastructure;
using INDMS.WebUI.Infrastructure.Filters;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers {
    [ExceptionHandler]
    public class MovementOrderController : Controller {
        private INDMSEntities db = new INDMSEntities();

        // GET: MovementOrder
        public ActionResult Index() {
            MovementOrderViewModel m = new MovementOrderViewModel();
            m.MovementOrders = db.MovementOrders;
            foreach (var item in m.MovementOrders) {
                item.InspectorName = db.Users.SingleOrDefault(d => d.UserId == new Guid(item.InspectorName)).Name;
                item.FirmName = db.Firms.SingleOrDefault(d => d.Id.ToString() == item.FirmName).FirmName;
                item.SigningAuthority = db.Users.SingleOrDefault(d => d.UserId == new Guid(item.SigningAuthority)).Name;
            }
            return View(m);
        }

        [AuthUser]
        public ActionResult New() {
            return View();
        }

        [HttpPost]
        [AuthUser]
        [ValidateAntiForgeryToken]
        public ActionResult New(MovementOrderViewModel m) {
            if (!string.IsNullOrEmpty(m.MovementOrder.OrderNo)) {
                if (!string.IsNullOrEmpty(m.MovementOrder.Subject)) {
                    if (!string.IsNullOrEmpty(m.MovementOrder.InspectorName)) {
                        if (!string.IsNullOrEmpty(m.MovementOrder.FirmName)) {
                            //Onward 
                            if (!string.IsNullOrEmpty(m.OnwordDateAndTime)) {
                                string[] OnwardDateTime = m.OnwordDateAndTime.Split('-');
                                m.MovementOrder.OnwardStartDate = Convert.ToDateTime(OnwardDateTime[0].Trim());
                                m.MovementOrder.OnwardEndDate = Convert.ToDateTime(OnwardDateTime[1].Trim());

                                if (!string.IsNullOrEmpty(m.MovementOrder.OnwordFrom)) {
                                    if (!string.IsNullOrEmpty(m.MovementOrder.OnwordTo)) {
                                        if (!string.IsNullOrEmpty(m.MovementOrder.OnwordModeOfTravel)) {
                                            if (m.MovementOrder.OnwordTo == "OTHER") {
                                                if (string.IsNullOrEmpty(m.OnwardModeOfTravel)) {
                                                    TempData["Error"] = "Please Enter Onward - Mode Of Travel.";
                                                    return View(m);
                                                }
                                                else {
                                                    m.MovementOrder.OnwordModeOfTravel = m.OnwardModeOfTravel.Trim();
                                                    AddParam("ModeOfTravel", m.OnwardModeOfTravel.Trim());
                                                }
                                            }

                                            //Return
                                            if (!string.IsNullOrEmpty(m.ReturnDateAndTime)) {
                                                string[] ReturnDateTime = m.ReturnDateAndTime.Split('-');
                                                m.MovementOrder.ReturnStartDate = Convert.ToDateTime(ReturnDateTime[0].Trim());
                                                m.MovementOrder.ReturnEndDate = Convert.ToDateTime(ReturnDateTime[1].Trim());

                                                if (!string.IsNullOrEmpty(m.MovementOrder.ReturnFrom)) {
                                                    if (!string.IsNullOrEmpty(m.MovementOrder.ReturnTo)) {
                                                        if (!string.IsNullOrEmpty(m.MovementOrder.ReturnModeOfTravel)) {
                                                            if (m.MovementOrder.ReturnTo == "OTHER") {
                                                                if (string.IsNullOrEmpty(m.ReturnModeOfTravel)) {
                                                                    TempData["Error"] = "Please Enter Return - Mode Of Travel.";
                                                                    return View(m);
                                                                }
                                                                else {
                                                                    m.MovementOrder.ReturnModeOfTravel = m.ReturnModeOfTravel.Trim();
                                                                    AddParam("ModeOfTravel", m.ReturnModeOfTravel.Trim());
                                                                }
                                                            }

                                                            if (!string.IsNullOrEmpty(m.MovementOrder.SigningAuthority)) {
                                                                try {
                                                                    m.MovementOrder.CreatedBy = Request.Cookies["INDMS"]["UserID"];
                                                                    m.MovementOrder.CreatedDate = DateTime.Now;

                                                                    m.MovementOrder.Designation = db.Users.SingleOrDefault(d => d.UserId == new Guid(m.MovementOrder.InspectorName)).Designation;
                                                                    m.MovementOrder.SADesignation = db.Users.SingleOrDefault(d => d.UserId == new Guid(m.MovementOrder.SigningAuthority)).Designation;


                                                                    if (ModelState.IsValid) {
                                                                        db.MovementOrders.Add(m.MovementOrder);
                                                                        db.SaveChanges();
                                                                        ModelState.Clear();
                                                                        TempData["MSG"] = "Saved Successfully.";
                                                                        return RedirectToAction("Index");
                                                                    }
                                                                }
                                                                catch (DbEntityValidationException e) {
                                                                    foreach (var eve in e.EntityValidationErrors) {
                                                                        Response.Write(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                                                            eve.Entry.Entity.GetType().Name, eve.Entry.State));
                                                                        foreach (var ve in eve.ValidationErrors) {
                                                                            Response.Write(string.Format("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                                                                ve.PropertyName,
                                                                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                                                                ve.ErrorMessage));
                                                                        }
                                                                    }
                                                                    throw;
                                                                }
                                                                catch (Exception ex) {
                                                                    Response.Write(ex);
                                                                    throw ex;
                                                                }
                                                            }
                                                            else {
                                                                TempData["Error"] = "Please select Signing Authority";
                                                            }
                                                        }
                                                        else {
                                                            TempData["Error"] = "Please select Return - Mode Of Travel.";
                                                        }
                                                    }
                                                    else {
                                                        TempData["Error"] = "Please select Return - To.";
                                                    }
                                                }
                                                else {
                                                    TempData["Error"] = "Please select Return - From.";
                                                }
                                            }
                                            else {
                                                TempData["Error"] = "Please enter Return Date & Time.";
                                            }
                                        }
                                        else {
                                            TempData["Error"] = "Please select Onward - Mode Of Travel.";
                                        }
                                    }
                                    else {
                                        TempData["Error"] = "Please select Onward - To.";
                                    }
                                }
                                else {
                                    TempData["Error"] = "Please select Onward - From.";
                                }
                            }
                            else {
                                TempData["Error"] = "Please enter Onward Date & Time.";
                            }
                        }
                        else {
                            TempData["Error"] = "Please Select valid Firm Name";
                        }
                    }
                    else {
                        TempData["Error"] = "Please Select valid Inspector Name.";
                    }
                }
                else {
                    TempData["Error"] = "Please enter valid Subject.";
                }
            }
            else {
                TempData["Error"] = "Please enter Valid Order Number";
            }

            return View(m);
        }

        [AuthUser]
        public ActionResult Edit(int? Id) {
            MovementOrderViewModel m = new MovementOrderViewModel();

            if (Id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                m.MovementOrder = db.MovementOrders.Find(Id);
                if (m.MovementOrder == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return View(m);
        }

        [HttpPost]
        [AuthUser]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovementOrderViewModel m) {
            if (!string.IsNullOrEmpty(m.MovementOrder.OrderNo)) {
                if (!string.IsNullOrEmpty(m.MovementOrder.Subject)) {
                    if (!string.IsNullOrEmpty(m.MovementOrder.InspectorName)) {
                        if (!string.IsNullOrEmpty(m.MovementOrder.FirmName)) {
                            //Onward 
                            if (!string.IsNullOrEmpty(m.OnwordDateAndTime)) {
                                string[] OnwardDateTime = m.OnwordDateAndTime.Split('-');
                                m.MovementOrder.OnwardStartDate = Convert.ToDateTime(OnwardDateTime[0].Trim());
                                m.MovementOrder.OnwardEndDate = Convert.ToDateTime(OnwardDateTime[1].Trim());

                                if (!string.IsNullOrEmpty(m.MovementOrder.OnwordFrom)) {
                                    if (!string.IsNullOrEmpty(m.MovementOrder.OnwordTo)) {
                                        if (!string.IsNullOrEmpty(m.MovementOrder.OnwordModeOfTravel)) {
                                            if (m.MovementOrder.OnwordTo == "OTHER") {
                                                if (string.IsNullOrEmpty(m.OnwardModeOfTravel)) {
                                                    TempData["Error"] = "Please Enter Onward - Mode Of Travel.";
                                                    return View(m);
                                                }
                                                else {
                                                    m.MovementOrder.OnwordModeOfTravel = m.OnwardModeOfTravel.Trim();
                                                    AddParam("ModeOfTravel", m.OnwardModeOfTravel.Trim());
                                                }
                                            }

                                            //Return
                                            if (!string.IsNullOrEmpty(m.ReturnDateAndTime)) {
                                                string[] ReturnDateTime = m.ReturnDateAndTime.Split('-');
                                                m.MovementOrder.ReturnStartDate = Convert.ToDateTime(ReturnDateTime[0].Trim());
                                                m.MovementOrder.ReturnEndDate = Convert.ToDateTime(ReturnDateTime[1].Trim());

                                                if (!string.IsNullOrEmpty(m.MovementOrder.ReturnFrom)) {
                                                    if (!string.IsNullOrEmpty(m.MovementOrder.ReturnTo)) {
                                                        if (!string.IsNullOrEmpty(m.MovementOrder.ReturnModeOfTravel)) {
                                                            if (m.MovementOrder.ReturnTo == "OTHER") {
                                                                if (string.IsNullOrEmpty(m.ReturnModeOfTravel)) {
                                                                    TempData["Error"] = "Please Enter Return - Mode Of Travel.";
                                                                    return View(m);
                                                                }
                                                                else {
                                                                    m.MovementOrder.ReturnModeOfTravel = m.ReturnModeOfTravel.Trim();
                                                                    AddParam("ModeOfTravel", m.ReturnModeOfTravel.Trim());
                                                                }
                                                            }

                                                            if (!string.IsNullOrEmpty(m.MovementOrder.SigningAuthority)) {
                                                                try {

                                                                    m.MovementOrder.UpdatedBy = Request.Cookies["INDMS"]["UserID"];
                                                                    m.MovementOrder.UpdatedDate = DateTime.Now.ToString();


                                                                    m.MovementOrder.Designation = db.Users.SingleOrDefault(d => d.UserId == new Guid(m.MovementOrder.InspectorName)).Designation;
                                                                    m.MovementOrder.SADesignation = db.Users.SingleOrDefault(d => d.UserId == new Guid(m.MovementOrder.SigningAuthority)).Designation;


                                                                    if (ModelState.IsValid) {
                                                                        using (var ctx = new INDMSEntities()) {
                                                                            ctx.Entry(m.MovementOrder).State = System.Data.Entity.EntityState.Modified;
                                                                            ctx.SaveChanges();
                                                                            ModelState.Clear();
                                                                            TempData["MSG"] = "Saved Successfully.";
                                                                            return RedirectToAction("Index");
                                                                        }
                                                                    }
                                                                }
                                                                catch (DbEntityValidationException e) {
                                                                    foreach (var eve in e.EntityValidationErrors) {
                                                                        Response.Write(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                                                            eve.Entry.Entity.GetType().Name, eve.Entry.State));
                                                                        foreach (var ve in eve.ValidationErrors) {
                                                                            Response.Write(string.Format("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                                                                ve.PropertyName,
                                                                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                                                                ve.ErrorMessage));
                                                                        }
                                                                    }
                                                                    throw;
                                                                }
                                                                catch (Exception ex) {
                                                                    Response.Write(ex);
                                                                    throw ex;
                                                                }
                                                            }
                                                            else {
                                                                TempData["Error"] = "Please select Signing Authority";
                                                            }
                                                        }
                                                        else {
                                                            TempData["Error"] = "Please select Return - Mode Of Travel.";
                                                        }
                                                    }
                                                    else {
                                                        TempData["Error"] = "Please select Return - To.";
                                                    }
                                                }
                                                else {
                                                    TempData["Error"] = "Please select Return - From.";
                                                }
                                            }
                                            else {
                                                TempData["Error"] = "Please enter Return Date & Time.";
                                            }
                                        }
                                        else {
                                            TempData["Error"] = "Please select Onward - Mode Of Travel.";
                                        }
                                    }
                                    else {
                                        TempData["Error"] = "Please select Onward - To.";
                                    }
                                }
                                else {
                                    TempData["Error"] = "Please select Onward - From.";
                                }
                            }
                            else {
                                TempData["Error"] = "Please enter Onward Date & Time.";
                            }
                        }
                        else {
                            TempData["Error"] = "Please Select valid Firm Name";
                        }
                    }
                    else {
                        TempData["Error"] = "Please Select valid Inspector Name.";
                    }
                }
                else {
                    TempData["Error"] = "Please enter valid Subject.";
                }
            }
            else {
                TempData["Error"] = "Please enter Valid Order Number";
            }

            return View(m);
        }

        public ActionResult Print(int? id) {
            MovementOrderViewModel m = new MovementOrderViewModel();

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                m.MovementOrders = (from d in db.MovementOrders
                                    where d.Id == (int)id
                                    select d).ToList();
                if (m.MovementOrders == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else {
                    foreach (var item in m.MovementOrders) {
                        item.InspectorName = db.Users.SingleOrDefault(d => d.UserId == new Guid(item.InspectorName)).Name;
                        item.FirmName = db.Firms.SingleOrDefault(d => d.Id.ToString() == item.FirmName).FirmName;
                        item.SigningAuthority = db.Users.SingleOrDefault(d => d.UserId == new Guid(item.SigningAuthority)).Name;
                    }

                    ReportDocument rd = new ReportDocument();
                    rd.Load(Path.Combine(Server.MapPath("~/Reports"), "MovementOrder.rpt"));
                    rd.SetDataSource(m.MovementOrders);

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try {
                        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                        stream.Seek(0, SeekOrigin.Begin);
                        return File(stream, "application/pdf", "MovementOrder.pdf");
                    }
                    catch (Exception ex) {
                        throw;
                    }
                }
            }
        }

        [AuthUser]
        [CAuthRole("Admin")]
        public ActionResult Approve() {
            MovementOrderViewModel m = new MovementOrderViewModel();
            m.MovementOrders = db.MovementOrders.Where(x => x.Flag != "ACCEPTED" && x.Flag != "REJECTED");
            foreach (var item in m.MovementOrders) {
                item.InspectorName = db.Users.SingleOrDefault(d => d.UserId == new Guid(item.InspectorName)).Name;
                item.FirmName = db.Firms.SingleOrDefault(d => d.Id.ToString() == item.FirmName).FirmName;
                item.SigningAuthority = db.Users.SingleOrDefault(d => d.UserId == new Guid(item.SigningAuthority)).Name;
            }
            return View(m);
        }

        [HttpPost]
        [AuthUser]
        [CAuthRole("Admin")]
        public ActionResult Approve(int? id) {
            string Msg = string.Empty;
            MovementOrderViewModel m = new MovementOrderViewModel();

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                m.MovementOrder = db.MovementOrders.Find(id);
                if (m.MovementOrder == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else {
                    m.MovementOrder.Flag = "ACCEPTED";
                    using (var ctx = new INDMSEntities()) {
                        ctx.Entry(m.MovementOrder).State = System.Data.Entity.EntityState.Modified;
                        ctx.SaveChanges();
                        Msg = "success";
                    }
                }

            }


            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthUser]
        [CAuthRole("Admin")]
        public ActionResult Reject(int? id) {
            string Msg = string.Empty;
            MovementOrderViewModel m = new MovementOrderViewModel();

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                m.MovementOrder = db.MovementOrders.Find(id);
                if (m.MovementOrder == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else {
                    m.MovementOrder.Flag = "REJECTED";
                    using (var ctx = new INDMSEntities()) {
                        ctx.Entry(m.MovementOrder).State = System.Data.Entity.EntityState.Modified;
                        ctx.SaveChanges();
                        Msg = "success";
                    }
                }

            }


            return Json(Msg, JsonRequestBehavior.AllowGet);
        }

        private static void AddParam(string strKeyName, string strKeyValue) {
            AddNewParameter obj = new AddNewParameter();
            string keyName = strKeyName;
            string keyValue = strKeyValue;
            obj.Add(keyName, keyValue);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}