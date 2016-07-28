using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INDMS.WebUI.Models;

namespace INDMS.WebUI.ViewModels
{
    public class POFlowViewModel
    {
        public POGeneration POGeneration { get; set; }
        public int POId { get; set; }

        public PurchaseOrder PO { get; set; }
    }
}