using INDMS.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INDMS.WebUI.ViewModels {
    public class UserViewModel {
       public IEnumerable<User> Users { get; set; }
    }
}