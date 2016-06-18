using INDMS.WebUI.Infrastructure.Encoding;
using INDMS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace INDMS.WebUI.Infrastructure {
    public class CAuthRole : AuthorizeAttribute {
        private readonly string[] allowRoles;
        private INDMSEntities db = new INDMSEntities();

        public CAuthRole(params string[] roles) {
            this.allowRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            bool authorise = false;
            string strUserId = string.Empty;

            if (HttpContext.Current.Request.Cookies["INDMS"] != null) {
                if (System.Text.Encoding.ASCII.DecodeBase64(HttpContext.Current.Request.Cookies["INDMS"]["UserName"]) == "admin") {
                    return authorise = true;
                }
                else {
                    strUserId = HttpContext.Current.Request.Cookies["INDMS"]["UserID"];
                    if (strUserId != "0") {
                        Guid userId = new Guid(strUserId);

                        List<User> users = new List<User>();

                        foreach (string role in allowRoles) {
                            var user = (from u in db.Users
                                        where u.UserId == userId && u.Role == role
                                        select u).ToList();
                            if (user.Any()) {
                                return authorise = true;
                            }
                        }
                    }
                    else if (strUserId == "0") {
                        return authorise = true;
                    }
                }
            }
            return authorise;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            if (HttpContext.Current.Request.Cookies["INDMS"] == null) {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            else {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            }
        }
    }
}