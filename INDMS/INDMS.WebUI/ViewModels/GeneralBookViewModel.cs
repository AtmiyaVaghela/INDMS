using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels {

    public class GeneralBookViewModel {
        public GeneralBook GeneralBook { get; set; }
        public IEnumerable<GeneralBook> GeneralBooks { get; set; }
        public string file { get; set; }
    }
}