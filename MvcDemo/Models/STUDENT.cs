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
    
    public partial class STUDENT
    {
        public STUDENT()
        {
            this.VOUCHER_TABLE = new HashSet<VOUCHER_TABLE>();
        }
    
        public string STUDENT_ID { get; set; }
        public int AVAIL_COURSE_ID { get; set; }
        public Nullable<int> ETHINICITY_ID { get; set; }
        public Nullable<int> DETAILED_ETHINICITY_ID { get; set; }
        public string STUDENT_FNAME { get; set; }
        public string STUDENT_LNAME { get; set; }
        public string GENDER { get; set; }
        public string DOB { get; set; }
        public string ADDRESS { get; set; }
        public string ACCOMODATION { get; set; }
        public string PHONE { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string CONTACT { get; set; }
    
        public virtual AVAILABLE_COURSE AVAILABLE_COURSE { get; set; }
        public virtual DETAILED_ETHINICITY_TABLE DETAILED_ETHINICITY_TABLE { get; set; }
        public virtual ETHINICITY_TABLE ETHINICITY_TABLE { get; set; }
        public virtual ICollection<VOUCHER_TABLE> VOUCHER_TABLE { get; set; }
    }
}
