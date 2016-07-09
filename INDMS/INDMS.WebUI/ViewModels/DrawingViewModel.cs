using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels {

    public class DrawingViewModel {
        public Drawing Drawing { get; set; }
        public IEnumerable<Drawing> Drawings { get; set; }
        public string file { get; set; }
    }
}