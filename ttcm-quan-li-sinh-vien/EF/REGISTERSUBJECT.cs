namespace ttcm_quan_li_sinh_vien.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("REGISTERSUBJECT")]
    public partial class REGISTERSUBJECT
    {
        [StringLength(20)]
        public string RegisterSubjectID { get; set; }

        [Required]
        [StringLength(20)]
        public string StudentID { get; set; }

        [StringLength(100)]
        public string TimeLearning { get; set; }

        [StringLength(100)]
        public string AddressLearn { get; set; }

        [StringLength(100)]
        public string SubjectName { get; set; }

        [StringLength(100)]
        public string TeacherName { get; set; }

        public bool? Status { get; set; }

        public virtual STUDENT STUDENT { get; set; }
    }
}
