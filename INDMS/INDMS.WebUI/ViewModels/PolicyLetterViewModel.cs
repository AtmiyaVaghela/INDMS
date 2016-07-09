using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels {

    public class PolicyLetterViewModel {
        public PolicyLetter PLetter { get; set; }
        public string OIssuingAutherity { get; set; }
        public IEnumerable<PolicyLetter> PolicyLetters { get; set; }
        public string file { get; set; }
    }
}