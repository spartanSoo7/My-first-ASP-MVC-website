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
    
    public partial class COURSE_TABLE
    {
        public COURSE_TABLE()
        {
            this.AVAILABLE_COURSE = new HashSet<AVAILABLE_COURSE>();
        }
    
        public int COURSE_ID { get; set; }
        public string COURSE_NAME { get; set; }
        public int FACULTY_ID { get; set; }
    
        public virtual ICollection<AVAILABLE_COURSE> AVAILABLE_COURSE { get; set; }
        public virtual FACULTY_TABLE FACULTY_TABLE { get; set; }
    }
}