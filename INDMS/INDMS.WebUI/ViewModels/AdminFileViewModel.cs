using INDMS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INDMS.WebUI.ViewModels
{
    public class AdminFileViewModel
    {
        public File File { get; set; }
        public IEnumerable<File> Files { get; set; }

        public string file { get; set; }
    }
}