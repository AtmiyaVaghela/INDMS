using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels
{
    public class FCLViewModel
    {
        public int POId { get; set; }
        public FCL FCL { get; set; }
        public IList<FCLDetail> FCLDetails { get; set; }
        public POGeneration POGeneration { get; set; }
        public PurchaseOrder PO { get; set; }

        public IEnumerable<FCL> FCLs { get; set; }
    }
}