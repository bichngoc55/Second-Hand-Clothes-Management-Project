namespace Second_Hand_Clothes_Management_Project.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MUAHANG")]
    public partial class MUAHANG
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string MAND { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MASP { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string MAKH { get; set; }

        public int? SL { get; set; }

        [Column(TypeName = "smalldatetime")]
        public System.DateTime NGAYBAN { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
