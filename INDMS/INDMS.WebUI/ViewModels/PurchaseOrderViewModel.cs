using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels {

    public class PurchaseOrderViewModel {
        public PurchaseOrder PurchaseOrder { get; set; }
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; }
        public string[] InspectorId { get; set; }
        public string OPOPlacingAuthority { get; set; }
    }
}