using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels
{
    public class QAPViewModel
    {
        public PurchaseOrder PO { get; set; }
        public QAP QAP { get; set; }
        public IEnumerable<QAP> QAPs { get; set; }
        public string file { get; set; }
        public int[] DrawingId { get; set; }
        public string DrawingNo { get; set; }
        public POGeneration POGeneration { get; set; }
    }
}