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
    
    public partial class NHAP
    {
        public string MASP { get; set; }
        public string MANHACUNGCAP { get; set; }
        public string MAKHO { get; set; }
        public Nullable<int> SL { get; set; }
        public Nullable<System.DateTime> NGNHAP { get; set; }
    
        public virtual KHO KHO { get; set; }
        public virtual NHACUNGCAP NHACUNGCAP { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}