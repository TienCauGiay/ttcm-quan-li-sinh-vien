namespace ttcm_quan_li_sinh_vien.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SCORE")]
    public partial class SCORE
    {
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string StudentID { get; set; }

        [Required(ErrorMessage = "Mã môn học không được để trống")]
        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string SubjectID { get; set; }

        public double? ScoreCC { get; set; }

        public double? ScoreKT { get; set; }

        public double? DiemThi { get; set; }

        public double? DiemTB { get; set; }

        public virtual STUDENT STUDENT { get; set; }

        public virtual SUBJECT SUBJECT { get; set; }
    }
}
