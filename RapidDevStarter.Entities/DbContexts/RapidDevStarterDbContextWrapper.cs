using Microsoft.EntityFrameworkCore;
using RapidDevStarter.Entities.RapidDevStarterEntities;

namespace RapidDevStarter.Entities.DbContexts
{
    /// <summary>
    /// Use wrapper to make necessary overrides without changing the automatically generated DbContext
    /// </summary>
    public class RapidDevStarterDbContextWrapper : RapidDevStarterDbContext
    {
        public RapidDevStarterDbContextWrapper()
        {
        }

        public RapidDevStarterDbContextWrapper(DbContextOptions<RapidDevStarterDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ContactInfo>(entity =>
            {
                // Make OnDelete behavior Cascade instead of ClientSetNull
                entity.HasOne(d => d.ContactInfoUserKeyNavigation)
                    .WithOne(p => p.ContactInfo)
                    .HasForeignKey<ContactInfo>(d => d.ContactInfoUserKey)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}