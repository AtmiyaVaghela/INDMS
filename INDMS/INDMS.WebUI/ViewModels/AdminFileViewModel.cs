using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels
{
    public class AdminFileViewModel
    {
        public File File { get; set; }
        public IEnumerable<File> Files { get; set; }

        public string file1 { get; set; }
    }
}