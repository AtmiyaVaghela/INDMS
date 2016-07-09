using INDMS.WebUI.Infrastructure;
using INDMS.WebUI.Infrastructure.Filters;
using INDMS.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace INDMS.WebUI.Controllers {

    public class HomeController : Controller {
        private INDMSEntities db = new INDMSEntities();

        // GET: Home
        [AuthUser]
        public ActionResult Dashboard() {
            return View();
        }

        public ActionResult GetJsonObjOfUpload() {
            int i = db.Drawings.Count()
                + db.GeneralBooks.Count()
                + db.GuideLines.Count()
                + db.PolicyLetters.Count()
                + db.QAPs.Count()
                + db.Standards.Count()
                + db.StandingOrders.Count();
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        [CAuthRole("Admin")]
        public ActionResult GetPendingMovementOrder() {
            int i = db.MovementOrders.Where(d => d.Flag != "ACCEPTED" && d.Flag != "REJECTED").Count();
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}