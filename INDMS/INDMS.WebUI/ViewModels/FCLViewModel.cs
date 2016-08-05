using INDMS.WebUI.Models;

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