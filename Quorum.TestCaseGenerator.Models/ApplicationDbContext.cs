using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Quorum.TestCaseGenerator.Models.Models;

namespace Quorum.TestCaseGenerator.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ControlMaster> ControlMasters { get; set; } = null!;
        public virtual DbSet<ControlMasterIp> ControlMasterIps { get; set; } = null!;
        public virtual DbSet<IpTypeMaster> IpTypeMasters { get; set; } = null!;
        public virtual DbSet<Person> Persons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=AP-EU40OUVMMTQA\\SQLEXPRESS;Initial Catalog=QRM_TST_GENERATOR; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ControlMaster>(entity =>
            {
                entity.ToTable("CONTROL_MASTER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CtrlNm)
                    .HasMaxLength(225)
                    .IsUnicode(false)
                    .HasColumnName("CTRL_NM");

                entity.Property(e => e.LogoPath)
                    .HasColumnType("text")
                    .HasColumnName("LOGO_PATH");
            });

            modelBuilder.Entity<ControlMasterIp>(entity =>
            {
                entity.ToTable("CONTROL_MASTER_IP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CrlId).HasColumnName("CRL_ID");

                entity.Property(e => e.CtrlIpName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("CTRL_IP_NAME");

                entity.Property(e => e.IsRequired)
                    .HasColumnName("IS_REQUIRED")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TypeId).HasColumnName("TYPE_ID");
            });

            modelBuilder.Entity<IpTypeMaster>(entity =>
            {
                entity.ToTable("IP_TYPE_MASTER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeNm)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_NM");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
