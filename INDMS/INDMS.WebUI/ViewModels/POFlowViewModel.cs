using INDMS.WebUI.Models;

namespace INDMS.WebUI.ViewModels
{
    public class POFlowViewModel
    {
        public POGeneration POGeneration { get; set; }
        public int POId { get; set; }
        public PurchaseOrder PO { get; set; }
        public FCL FCL { get; set; }
        public QAP QAP { get; set; }
    }
}