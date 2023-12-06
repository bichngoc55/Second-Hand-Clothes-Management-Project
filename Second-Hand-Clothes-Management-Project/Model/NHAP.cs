namespace Second_Hand_Clothes_Management_Project.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHAP")]
    public partial class NHAP
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MASP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MANHACUNGCAP { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string MAKHO { get; set; }

        public int? SL { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGNHAP { get; set; }

        public virtual KHO KHO { get; set; }

        public virtual NHACUNGCAP NHACUNGCAP { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
