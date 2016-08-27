using INDMS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INDMS.WebUI.ViewModels
{
    public class INOTEViewModel
    {
        public CoveringLetter CoveringLetter { get; set; }
        public IEnumerable<CoveringLetter> CoveringLetters { get; set; }

        public POGeneration POGeneration { get; set; }

        public PurchaseOrder PO { get; set; }
    }
}