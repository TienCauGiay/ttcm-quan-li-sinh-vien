namespace ttcm_quan_li_sinh_vien.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TEACHER")]
    public partial class TEACHER
    {
        [StringLength(20)]
        public string TeacherID { get; set; }

        [StringLength(20)]
        public string FacultyID { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public virtual FACULTY FACULTY { get; set; }
    }
}
