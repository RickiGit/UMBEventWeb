//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EventApps.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Certificate
    {
        public System.Guid ID { get; set; }
        public System.Guid IDEvent { get; set; }
        public string Images { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual Event Event { get; set; }
    }
}
