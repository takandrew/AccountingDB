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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=AccountingDB.db;");
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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
