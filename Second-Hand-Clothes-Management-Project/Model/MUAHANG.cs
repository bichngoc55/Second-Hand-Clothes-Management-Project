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
    
    public partial class MUAHANG
    {
        public string MANV { get; set; }
        public string MASP { get; set; }
        public string MAKH { get; set; }
        public Nullable<int> SL { get; set; }
        public Nullable<System.DateTime> NGAYBAN { get; set; }
    
        public virtual KHACHHANG KHACHHANG { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}