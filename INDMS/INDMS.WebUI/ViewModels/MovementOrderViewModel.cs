using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels {

    public class MovementOrderViewModel {
        public MovementOrder MovementOrder { get; set; }
        public string OnwordDateAndTime { get; set; }
        public string ReturnDateAndTime { get; set; }
        public string ReturnModeOfTravel { get; set; }
        public string OnwardModeOfTravel { get; set; }
        public IEnumerable<MovementOrder> MovementOrders { get; set; }
    }
}