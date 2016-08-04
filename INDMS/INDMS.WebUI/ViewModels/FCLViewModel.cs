using INDMS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INDMS.WebUI.ViewModels
{
    public class FCLViewModel
    {
        public int POId { get; set; }
        public FCL FCL { get; set; }
        public POGeneration POGeneration { get; set; }
        public PurchaseOrder PO { get; set; }
    }
}