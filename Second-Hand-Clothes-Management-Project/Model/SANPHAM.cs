namespace Second_Hand_Clothes_Management_Project.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            MUAHANGs = new HashSet<MUAHANG>();
            NHAPs = new HashSet<NHAP>();
        }

        [Key]
        [StringLength(20)]
        public string MASP { get; set; }

        [Required]
        [StringLength(50)]
        public string TENSP { get; set; }

        [Column(TypeName = "money")]
        public decimal GIA { get; set; }

        public int SL { get; set; }

        [StringLength(20)]
        public string LOAISP { get; set; }

        [StringLength(5)]
        public string SIZE { get; set; }

        [StringLength(100)]
        public string MOTA { get; set; }

        public string HINHSP { get; set; }

        [StringLength(20)]
        public string MAGIAMGIA { get; set; }

        public virtual GIAMGIA GIAMGIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUAHANG> MUAHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAP> NHAPs { get; set; }
    }
}
