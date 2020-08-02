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
                    .HasConstraintName("FK__ContactIn__Conta__5FB337D6");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserKey)
                    .HasName("PK__tmp_ms_x__296ADCF17D6C2C37");

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

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
