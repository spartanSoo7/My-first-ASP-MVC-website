//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcDemo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ETHINICITY_TABLE
    {
        public ETHINICITY_TABLE()
        {
            this.DETAILED_ETHINICITY_TABLE = new HashSet<DETAILED_ETHINICITY_TABLE>();
            this.STUDENTS = new HashSet<STUDENT>();
        }
    
        public int ETHINICITY_ID { get; set; }
        public string ETHINICITY { get; set; }
    
        public virtual ICollection<DETAILED_ETHINICITY_TABLE> DETAILED_ETHINICITY_TABLE { get; set; }
        public virtual ICollection<STUDENT> STUDENTS { get; set; }
    }
}
