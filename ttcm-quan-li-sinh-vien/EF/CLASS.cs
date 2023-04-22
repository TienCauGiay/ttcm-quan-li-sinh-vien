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
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string TeacherID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string StudentID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual STUDENT STUDENT { get; set; }

        public virtual TEACHER TEACHER { get; set; }
    }
}
