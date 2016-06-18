using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INDMS.WebUI.Models {
    public class ExceptionLogger {
        public string ExceptionMessage { get; set; }
        public string ControllerName { get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime LogTime { get; set; }
    }
}