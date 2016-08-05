using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels
{
    public class AdminCorrespondenceViewModel
    {
        public AdminCorrespondence AdminCorrespondence { get; set; }
        public IEnumerable<AdminCorrespondence> AdminCorrespondences { get; set; }
        public string file { get; set; }
    }
}