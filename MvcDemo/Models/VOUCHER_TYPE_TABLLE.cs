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
    
    public partial class VOUCHER_TYPE_TABLLE
    {
        public VOUCHER_TYPE_TABLLE()
        {
            this.VOUCHER_TABLE = new HashSet<VOUCHER_TABLE>();
        }
    
        public int VOUCHER_TYPE_ID { get; set; }
        public string VOUCHER_TYPE { get; set; }
        public string VOUCHER_SPECS { get; set; }
    
        public virtual ICollection<VOUCHER_TABLE> VOUCHER_TABLE { get; set; }
    }
}
