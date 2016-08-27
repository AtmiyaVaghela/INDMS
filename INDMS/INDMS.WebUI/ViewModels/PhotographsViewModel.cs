using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels
{
    public class PhotographsViewModel
    {
        public Photograph Photograph { get; set; }
        public IEnumerable<Photograph> Photographs { get; set; }

        public string File { get; set; }

        public PurchaseOrder PO { get; set; }
    }
}