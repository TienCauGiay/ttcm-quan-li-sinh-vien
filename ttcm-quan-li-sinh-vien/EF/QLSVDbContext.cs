using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ttcm_quan_li_sinh_vien.EF
{
    public partial class QLSVDbContext : DbContext
    {
        public QLSVDbContext()
            : base("name=QLSVDbContext")
        {
        }

        public virtual DbSet<CLASS> CLASSes { get; set; }
        public virtual DbSet<FACULTY> FACULTies { get; set; }
        public virtual DbSet<REGISTERSUBJECT> REGISTERSUBJECTs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SCORE> SCOREs { get; set; }
        public virtual DbSet<SEMESTER> SEMESTERs { get; set; }
        public virtual DbSet<STUDENT> STUDENTs { get; set; }
        public virtual DbSet<SUBJECT> SUBJECTs { get; set; }
        public virtual DbSet<TEACHER> TEACHERs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CLASS>()
                .HasMany(e => e.REGISTERSUBJECTs)
                .WithRequired(e => e.CLASS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<STUDENT>()
                .HasMany(e => e.SCOREs)
                .WithRequired(e => e.STUDENT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUBJECT>()
                .HasMany(e => e.REGISTERSUBJECTs)
                .WithRequired(e => e.SUBJECT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUBJECT>()
                .HasMany(e => e.SCOREs)
                .WithRequired(e => e.SUBJECT)
                .WillCascadeOnDelete(false);
        }
    }
}
