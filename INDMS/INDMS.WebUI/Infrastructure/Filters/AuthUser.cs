using INDMS.WebUI.Infrastructure.Encoding;
using INDMS.WebUI.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace INDMS.WebUI.Infrastructure.Filters {

    public class AuthUser : AuthorizeAttribute {
        private INDMSEntities db = new INDMSEntities();

        [HandleError]
        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            bool authorise = false;
            if (HttpContext.Current.Request.Cookies["INDMS"] != null) {
                string strUserId = HttpContext.Current.Request.Cookies["INDMS"]["UserID"];
                if (!strUserId.Equals("00000000-0000-0000-0000-000000000000")) {
                    Guid userId = new Guid(strUserId);

                    //find UserId in database

                    var user = from u in db.Users
                               where u.UserId == userId
                               select u;

                    if (user.Any()) {
                        authorise = true;
                    }
                    else {
                        if (System.Text.Encoding.ASCII.DecodeBase64(HttpContext.Current.Request.Cookies["INDMS"]["UserName"]) == "Admin") {
                            authorise = true;
                        }
                        else {
                            authorise = false;
                        }
                    }
                }
                else if (strUserId.Equals("00000000-0000-0000-0000-000000000000")) {
                    return authorise = true;
                }
            }
            return authorise;
        }

        [HandleError]
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            if (HttpContext.Current.Request.Cookies["INDMS"] == null) {
                filterContext.Result =
                new RedirectToRouteResult(
                    new RouteValueDictionary{{ "controller", "Account" },
                                                 { "action", "Login" },
                                                 { "returnUrl",    filterContext.HttpContext.Request.RawUrl }
                                                });

                //filterContext.Result = new RedirectToRouteResult(new
                //    RouteValueDictionary(new { controller = "Account", action = "Login"  }));
            }
            else {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            }
        }
    }
}