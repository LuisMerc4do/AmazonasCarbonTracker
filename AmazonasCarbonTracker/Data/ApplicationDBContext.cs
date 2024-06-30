using Microsoft.EntityFrameworkCore;
using AmazonasCarbonTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace AmazonasCarbonTracker.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<EmissionRecord> EmissionRecords { get; set; }
        public DbSet<SustainabilityMetrics> SustainabilityMetrics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EmissionRecord>()
                .Property(e => e.Date)
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Entity<EmissionRecord>()
                .HasKey(e => e.Id);

            builder.Entity<SustainabilityMetrics>()
                .HasKey(s => s.Id);

            builder.Entity<EmissionRecord>()
                .HasOne(e => e.AppUser)
                .WithMany(u => u.EmissionRecords)
                .HasForeignKey(e => e.AppUserId);

            builder.Entity<SustainabilityMetrics>()
                .HasOne(s => s.AppUser)
                .WithMany(u => u.SustainabilityMetrics)
                .HasForeignKey(s => s.AppUserId);

            SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Company",
                    NormalizedName = "COMPANY"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
