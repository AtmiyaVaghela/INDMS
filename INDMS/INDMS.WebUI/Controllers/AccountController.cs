using INDMS.WebUI.Infrastructure.Encoding;
using INDMS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers
{

    public class AccountController : Controller
    {
        private INDMSEntities db = new INDMSEntities();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string userName = user.UserName;
                string password = System.Text.Encoding.ASCII.EncodeBase64(user.Password);
                if (user.UserName.Equals(ConfigurationManager.AppSettings["UserName"].ToString()) && user.Password.Equals(ConfigurationManager.AppSettings["Password"].ToString()))
                {
                    CreateSession(user);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Home");
                    }
                }
                else
                {
                    try
                    {
                        db.Database.Connection.Open();
                        User _user = db.Users.Where(i => i.UserName == userName && i.Password == password && i.Active != "N").SingleOrDefault();

                        if (_user != null)
                        {
                            CreateSession(_user);
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Dashboard", "Home");
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write(ex.StackTrace);
                    } 
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            Response.Cookies["INDMS"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Login");
        }

        private void CreateSession(User user)
        {
            Response.Cookies["INDMS"]["UserID"] = user.UserId.ToString();
            Response.Cookies["INDMS"]["UserName"] = System.Text.Encoding.ASCII.EncodeBase64(user.UserName);
            Response.Cookies["INDMS"]["Name"] = System.Text.Encoding.ASCII.EncodeBase64(user.Name);
            Response.Cookies["INDMS"]["Role"] = System.Text.Encoding.ASCII.EncodeBase64(user.Role);
            Response.Cookies["INDMS"].Expires = DateTime.Now.AddMinutes(30);
        }

        [HttpPost]
        public ActionResult GetJsonObjOfRoles()
        {
            IEnumerable<string> KeyValueList = db.Roles.Select(x => x.Role1).ToList<string>();

            return Json(KeyValueList, JsonRequestBehavior.AllowGet);
        }
    }
}