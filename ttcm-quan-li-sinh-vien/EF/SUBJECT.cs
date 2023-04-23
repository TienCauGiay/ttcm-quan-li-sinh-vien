namespace ttcm_quan_li_sinh_vien.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SUBJECT")]
    public partial class SUBJECT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUBJECT()
        {
            SCOREs = new HashSet<SCORE>();
        }

        [StringLength(20)]
        public string SubjectID { get; set; }

        [StringLength(20)]
        public string SemesterID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? NumberTC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCORE> SCOREs { get; set; }

        public virtual SEMESTER SEMESTER { get; set; }
    }
}
