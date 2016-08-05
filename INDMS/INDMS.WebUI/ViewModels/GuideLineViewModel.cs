using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels
{
    public class GuideLineViewModel
    {
        public GuideLine GuideLine { get; set; }
        public string OIssuingAutherity { get; set; }
        public IEnumerable<GuideLine> GuideLines { get; set; }
        public string file { get; set; }
    }
}