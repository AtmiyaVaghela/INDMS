//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace INDMS.WebUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Photograph
    {
        public decimal Id { get; set; }
        public Nullable<decimal> POId { get; set; }
        public string PONo { get; set; }
        public string FolderName { get; set; }
        public string Subject { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string FilePath { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
