//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Second_Hand_Clothes_Management_Project.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class KHO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHO()
        {
            this.NHAPs = new HashSet<NHAP>();
        }
    
        public string MAKHO { get; set; }
        public string TENKHO { get; set; }
        public string DIADIEM { get; set; }
        public Nullable<int> SLTONKHO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAP> NHAPs { get; set; }
    }
}