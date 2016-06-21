using INDMS.WebUI.Infrastructure;
using INDMS.WebUI.Infrastructure.Encoding;
using INDMS.WebUI.Infrastructure.Filters;
using INDMS.WebUI.Models;
using INDMS.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers {
    public class UtilityController : Controller {
        private INDMSEntities db = new INDMSEntities();

        [CAuthRole("Admin")]
        public ActionResult UserList() {
            UserViewModel u = new UserViewModel();
            u.Users = from d in db.Users
                     
                      orderby d.Name
                      select d;

            return View(u);
        }

        [HttpPost]
        public ActionResult ChangeStatus(User m) {
            User u = db.Users.Find(m.UserId);
            u.Active = m.Active;

            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();

            ModelState.Clear();

            return Json(m.UserId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllDesignation() {
            IEnumerable<string> designations = from d in db.Users
                                               where d.Designation != null
                                     select d.Designation;

            return Json(designations, JsonRequestBehavior.AllowGet);
        }

        // GET: Utility
        [CAuthRole("Admin")]
        public ActionResult CreateUser() {
            PopulateRoleDropDownList();

            return View();
        }

        private void PopulateRoleDropDownList() {
            var roleQuery = from d in db.Roles
                            orderby d.Role1
                            select d;

            ViewBag.Role = new SelectList(roleQuery, "Role1", "Role1", string.Empty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CAuthRole("Admin")]
        public ActionResult CreateUser(User user, string ODesignation) {
            if (ModelState.IsValid) {
                try {
                    user.UserId = Guid.NewGuid();
                    user.Designation = user.Designation.Equals("OTHERS") ? ODesignation : user.Designation;
                    user.Password = System.Text.Encoding.ASCII.EncodeBase64(user.UserName);
                    user.CreatedBy = Request.Cookies["INDMS"]["UserID"];

                    IEnumerable<User> u = from d in db.Users
                                          where d.UserName == user.UserName
                                          select d;

                    if (u.Count() < 1) {
                        db.Users.Add(user);
                        db.SaveChanges();
                        TempData["MSG"] = "User Saved.";
                        return RedirectToAction("UserList");
                    }
                    else {
                        TempData["Error"] = "User Name Already Used.";
                    }
                }
                catch (Exception ex) {
                    TempData["Error"] = ex.Message;
                }
            }
            PopulateRoleDropDownList();
            return View(new User());
        }

        [CAuthRole("Admin")]
        [HttpGet]
        public ActionResult UserEdit(string userId) {
            if (userId == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                User u = db.Users.Find(new Guid(userId));
                if (u == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else 
                {
                    PopulateRoleDropDownList();
                    return View(u);
                }
            }
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CAuthRole("Admin")]
        public ActionResult UserEdit(User user, string ODesignation) {
            if (ModelState.IsValid) {
                try {
                    
                    user.Designation = user.Designation.Equals("OTHERS") ? ODesignation : user.Designation;
                    
                    user.CreatedBy = Request.Cookies["INDMS"]["UserID"];

                    IEnumerable<User> u = from d in db.Users
                                          where d.UserName == user.UserName
                                          select d;

                    if (u.Count() == 1) {
                        try {
                            db.Entry(user).State = EntityState.Modified;

                            db.SaveChanges();
                            ModelState.Clear();

                            TempData["MSG"] = "User Saved.";
                            return RedirectToAction("UserList");
                        }
                        catch (RetryLimitExceededException /* dex */) {
                            TempData["Error"] = "Unable to save changes. Try again, and if the problem persists, see your system administrator.";
                        }
                        catch (Exception ex) {
                            TempData["Error"] = ex.Message;
                        }
                      
                    }
                    else {
                        TempData["Error"] = "User Name Already Used.";
                    }
                }
                catch (Exception ex) {
                    TempData["Error"] = ex.Message;
                }
            }
            PopulateRoleDropDownList();
            return View(new User());
        }

        [AuthUser]
        public ActionResult ChangePassword() {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword cp) {
            if (ModelState.IsValid) {
                if (!string.IsNullOrEmpty(cp.CurrentPassword) && !string.IsNullOrEmpty(cp.NewPassword) && !string.IsNullOrEmpty(cp.ReEnterPassword)) {
                    User user = db.Users.Find(new Guid(Request.Cookies["INDMS"]["UserID"]));

                    if (user != null) {
                        if (System.Text.Encoding.ASCII.EncodeBase64(cp.CurrentPassword).Equals(user.Password)) {
                            user.Password = System.Text.Encoding.ASCII.EncodeBase64(cp.NewPassword);
                            try {
                                db.SaveChanges();
                                TempData["MSG"] = "Password changed successfully.";
                                return RedirectToAction("ChangePassword");
                            }
                            catch (RetryLimitExceededException /* dex */) {
                                //Log the error (uncomment dex variable name and add a line here to write a log.
                                TempData["Error"] = "Unable to save changes. Try again, and if the problem persists, see your system administrator.";
                            }
                        }
                        else {
                            TempData["Error"] = "Current Password not matched.";
                        }
                    }
                }
                else {
                    TempData["Error"] = "All Field are required.";
                }
            }
            return View();
        }

        [AuthUser]
        public ActionResult Profile() {
            if (Request.Cookies["INDMS"] != null) {
                ProfileViewModel m = new ProfileViewModel();

                m.User = new Models.User();

                m.User.UserId = new Guid(Request.Cookies["INDMS"]["UserID"]);

                m.User = db.Users.SingleOrDefault(x => x.UserId == m.User.UserId);

                return View(m);
            }
            else {
                return RedirectToAction("Logout", "Account");
            }
        }

        [HttpPost]
        [AuthUser]
        public ActionResult Profile(ProfileViewModel m, HttpPostedFileBase inputFile) {
            if (inputFile != null && inputFile.ContentLength > 0) {
                if (inputFile.ContentType == "image/jpeg") {
                    System.IO.File.Delete(Server.MapPath("~/ProfilePic/") + Request.Cookies["INDMS"]["UserID"] + ".JPG");
                    inputFile.SaveAs(Server.MapPath("~/ProfilePic/") + Request.Cookies["INDMS"]["UserID"] + ".JPG");
                }
            }
            if (!string.IsNullOrEmpty(m.User.Name)) {
                if (!string.IsNullOrEmpty(m.User.Role)) {
                    try {
                        User u = db.Users.Find(m.User.UserId);

                        if (u != null) {
                            u.Name = m.User.Name;
                            u.Role = m.User.Role;
                            u.Email = m.User.Email;

                            db.SaveChanges();
                            TempData["MSG"] = "Profile Updated.";

                            return RedirectToAction("Profile");
                        }
                    }
                    catch (RetryLimitExceededException /* dex */) {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        TempData["Error"] = "Unable to save changes. Try again, and if the problem persists, see your system administrator.";
                    }
                }
                else {
                    TempData["Error"] = "Please select Role";
                }
            }
            else {
                TempData["Error"] = "Please enter Name.";
            }
            return View(m);
        }
    }
}