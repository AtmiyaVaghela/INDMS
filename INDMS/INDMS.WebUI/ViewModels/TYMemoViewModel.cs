using INDMS.WebUI.Models;
using System.Collections.Generic;

namespace INDMS.WebUI.ViewModels
{
    public class TYMemoViewModel
    {
        public TYMemo TYMemo { get; set; }
        public IEnumerable<TYMemo> TYMemos { get; set; }

        public string File { get; set; }
    }
}