namespace ttcm_quan_li_sinh_vien.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("STUDENT")]
    public partial class STUDENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STUDENT()
        {
            REGISTERSUBJECTs = new HashSet<REGISTERSUBJECT>();
            SCOREs = new HashSet<SCORE>();
        }

        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(20)]
        public string StudentID { get; set; }

        [Required(ErrorMessage = "Mã lớp không được để trống")]
        [StringLength(20)]
        public string ClassID { get; set; }

        [StringLength(30)]
        public string FullName { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        [Column(TypeName = "date")]
        public DateTime? YearAdmission { get; set; }

        public virtual CLASS CLASS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REGISTERSUBJECT> REGISTERSUBJECTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCORE> SCOREs { get; set; }
    }
}
