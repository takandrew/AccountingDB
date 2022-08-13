using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Accounting
{
    public partial class AccountingDBContext : DbContext
    {
        public AccountingDBContext()
        {
        }

        public AccountingDBContext(DbContextOptions<AccountingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountingEntity> AccountingEntities { get; set; } = null!;
        public virtual DbSet<StatusList> StatusLists { get; set; } = null!;
        public virtual DbSet<TypeList> TypeLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=AccountingDB.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountingEntity>(entity =>
            {
                entity.ToTable("AccountingEntity");

                entity.HasIndex(e => e.Id, "IX_AccountingEntity_ID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateAdded).HasColumnName("Date_Added");

                entity.Property(e => e.DateChanged).HasColumnName("Date_Changed");

                entity.Property(e => e.NumberOfRe).HasColumnName("Number_of_Re");

                entity.Property(e => e.ProgressCur).HasColumnName("Progress_Cur");

                entity.Property(e => e.ProgressSum).HasColumnName("Progress_Sum");

                entity.Property(e => e.ReleaseYear).HasColumnName("Release_Year");

                entity.Property(e => e.StatusId).HasColumnName("Status_ID");

                entity.Property(e => e.TypeId).HasColumnName("Type_ID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.AccountingEntities)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AccountingEntities)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<StatusList>(entity =>
            {
                entity.ToTable("StatusList");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<TypeList>(entity =>
            {
                entity.ToTable("TypeList");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
