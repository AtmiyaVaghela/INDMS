using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels
{
    public class StandardViewModel
    {
        public Standard Standard { get; set; }
        public string OSubject { get; set; }
        public string OType { get; set; }
        public IEnumerable<Standard> Standards { get; set; }

        public string file { get; set; }
    }
}