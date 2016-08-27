using INDMS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INDMS.WebUI.ViewModels
{
    public class POCorrespondenceViewModel
    {
        public POCorrespondence POCorrespondence { get; set; }

        public IEnumerable<POCorrespondence> POCorrespondences { get; set; }

        public POGeneration POGeneration { get; set; }

        public PurchaseOrder PO { get; set; }

        public string file { get; set; }
    }
}