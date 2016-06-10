using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INDMS.Domain.Entities
{
    public enum Role
    {
        Admin, General
    }

    public class User
    {
        public Guid UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }        
        public string Active { get; set; }
    }

    

}
