using Microsoft.EntityFrameworkCore;
using RapidDevStarter.Entities.RapidDevStarterEntities;

namespace RapidDevStarter.Entities.DbContexts
{
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
                entity.HasOne(d => d.ContactInfoUserKeyNavigation)
                    .WithOne(p => p.ContactInfo)
                    .HasForeignKey<ContactInfo>(d => d.ContactInfoUserKey)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}