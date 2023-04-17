namespace ttcm_quan_li_sinh_vien.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CLASS")]
    public partial class CLASS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLASS()
        {
            REGISTERSUBJECTs = new HashSet<REGISTERSUBJECT>();
        }

        [StringLength(20)]
        public string ClassID { get; set; }

        [StringLength(20)]
        public string TeacherID { get; set; }

        [StringLength(20)]
        public string StudentID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual STUDENT STUDENT { get; set; }

        public virtual TEACHER TEACHER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REGISTERSUBJECT> REGISTERSUBJECTs { get; set; }
    }
}
