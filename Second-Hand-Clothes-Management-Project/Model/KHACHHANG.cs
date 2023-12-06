namespace Second_Hand_Clothes_Management_Project.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            MUAHANGs = new HashSet<MUAHANG>();
        }

        [Key]
        [StringLength(20)]
        public string MAKH { get; set; }

        [Required]
        [StringLength(50)]
        public string TENKH { get; set; }

        [StringLength(5)]
        public string GIOITINH { get; set; }

        [StringLength(200)]
        public string DIACHI { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [Required]
        [StringLength(20)]
        public string LOAIKH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUAHANG> MUAHANGs { get; set; }
    }
}
