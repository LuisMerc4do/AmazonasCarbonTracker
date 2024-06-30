using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using AmazonasCarbonTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace AmazonasCarbonTracker.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<EmissionRecord> EmissionRecords { get; set; }
        public DbSet<SustainabilityMetrics> SustainabilityMetrics { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure relationships
            builder.Entity<EmissionRecord>()
                .HasOne(er => er.AppUser)
                .WithMany(u => u.EmissionRecords)
                .HasForeignKey(er => er.AppUserId);

            builder.Entity<SustainabilityMetrics>()
                .HasOne(sm => sm.AppUser)
                .WithMany(u => u.SustainabilityMetrics)
                .HasForeignKey(sm => sm.AppUserId);

            List<IdentityRole> roles = new List<IdentityRole>()
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
