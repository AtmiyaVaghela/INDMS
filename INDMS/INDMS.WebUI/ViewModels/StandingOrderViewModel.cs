using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels
{
    public class StandingOrderViewModel
    {
        public StandingOrder StandingOrder { get; set; }
        public string OIssuingAutherity { get; set; }
        public string OSubject { get; set; }
        public IEnumerable<StandingOrder> StandingOrders { get; set; }

        public string file { get; set; }
    }
}