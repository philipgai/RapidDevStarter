using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RapidDevStarter.Entities.RapidDevStarterEntities
{
    public partial class RapidDevStarterDbContext : DbContext
    {
        public RapidDevStarterDbContext()
        {
        }

        public RapidDevStarterDbContext(DbContextOptions<RapidDevStarterDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContactInfo> ContactInfo { get; set; }
        public virtual DbSet<DboContactInfo> DboContactInfo { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=RapidDevStarter;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.HasKey(e => e.ContactInfoUserKey)
                    .HasName("PK__tmp_ms_x__1886ADBC3E74ABA9");

                entity.Property(e => e.ContactInfoUserKey).ValueGeneratedNever();

                entity.Property(e => e.City).HasMaxLength(64);

                entity.Property(e => e.Country).HasMaxLength(64);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Email).HasMaxLength(64);

                entity.Property(e => e.Phone).HasMaxLength(64);

                entity.Property(e => e.PostalCode).HasMaxLength(64);

                entity.Property(e => e.State).HasMaxLength(64);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.ContactInfoUserKeyNavigation)
                    .WithOne(p => p.ContactInfo)
                    .HasForeignKey<ContactInfo>(d => d.ContactInfoUserKey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ContactIn__Conta__6754599E");
            });

            modelBuilder.Entity<DboContactInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dbo_ContactInfo", "History");

                entity.HasIndex(e => new { e.RowEnd, e.RowStart })
                    .HasName("ix_dbo_ContactInfo")
                    .IsClustered();

                entity.Property(e => e.City).HasMaxLength(64);

                entity.Property(e => e.Country).HasMaxLength(64);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Email).HasMaxLength(64);

                entity.Property(e => e.Phone).HasMaxLength(64);

                entity.Property(e => e.PostalCode).HasMaxLength(64);

                entity.Property(e => e.RowEnd).HasColumnType("datetime2(2)");

                entity.Property(e => e.RowStart).HasColumnType("datetime2(2)");

                entity.Property(e => e.State).HasMaxLength(64);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserKey)
                    .HasName("PK__tmp_ms_x__296ADCF1B20651FF");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.MiddleName).HasMaxLength(64);

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasDefaultValueSql("(suser_name())");

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getutcdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
