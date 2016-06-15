using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INDMS.WebUI.Models;

namespace INDMS.WebUI.ViewModels {
    public class QAPViewModel {
        public QAP QAP { get; set; }
        public IEnumerable<QAP> QAPs { get; set; }
        public string file { get; set; }
    }
}