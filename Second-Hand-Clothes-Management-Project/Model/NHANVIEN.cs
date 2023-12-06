namespace Second_Hand_Clothes_Management_Project.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHANVIEN")]
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            MUAHANGs = new HashSet<MUAHANG>();
        }

        [Key]
        [StringLength(20)]
        public string MANV { get; set; }

        [Required]
        [StringLength(50)]
        public string TENNV { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGSINH { get; set; }

        [StringLength(5)]
        public string GIOITINH { get; set; }

        [Required]
        [StringLength(200)]
        public string DIACHI { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(30)]
        public string EMAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUAHANG> MUAHANGs { get; set; }
    }
}
