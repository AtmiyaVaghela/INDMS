using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels
{
    public class POFlowViewModel
    {
        public POGeneration POGeneration { get; set; }
        public int POId { get; set; }
        public PurchaseOrder PO { get; set; }
        public IEnumerable<FCL> FCLs { get; set; }
        public IEnumerable<FCLDetail> FCLDetails { get; set; }
        public QAP QAP { get; set; }
    }
}