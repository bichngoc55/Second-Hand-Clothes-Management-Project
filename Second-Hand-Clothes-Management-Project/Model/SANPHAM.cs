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
    
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.MUAHANGs = new HashSet<MUAHANG>();
            this.NHAPs = new HashSet<NHAP>();
        }
    
        public string MASP { get; set; }
        public string TENSP { get; set; }
        public decimal GIA { get; set; }
        public int SL { get; set; }
        public string LOAISP { get; set; }
        public string SIZE { get; set; }
        public string MOTA { get; set; }
        public string HINHSP { get; set; }
        public string MAGIAMGIA { get; set; }
    
        public virtual GIAMGIA GIAMGIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUAHANG> MUAHANGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAP> NHAPs { get; set; }
    }
}
