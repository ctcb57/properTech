using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using properTech.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace properTech.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Manager> Manager { get; set; }

        public DbSet<Resident> Resident { get; set; }

        public DbSet<MaintenanceTech> MaintenanceRequest { get; set; }

        public DbSet<Property> Property { get; set; }

        public DbSet<Building> Building { get; set; }

        public DbSet<Unit> Unit { get; set; }
        public DbSet<MonthlyRevenue> MonthlyRevenue { get; set; }
        public DbSet<OccupancyPercent> OccupancyPercent { get; set; }
        public DbSet<QuarterlyEarnings> QuarterlyEarnings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MonthlyRevenue>().HasData(
                new MonthlyRevenue
                {
                    Id = 1,
                    Month = "January",
                    Revenue = 5000
                },
                new MonthlyRevenue
                {
                    Id = 2,
                    Month = "February",
                    Revenue = 5400
                },
                new MonthlyRevenue
                {
                    Id = 3,
                    Month = "March",
                    Revenue = 4800
                },
                new MonthlyRevenue
                {
                    Id = 4,
                    Month = "April",
                    Revenue = 6000
                },
                new MonthlyRevenue
                {
                    Id = 5,
                    Month = "May",
                    Revenue = 5000
                },
                new MonthlyRevenue
                {
                    Id = 6,
                    Month = "June",
                    Revenue = 4200
                },
                new MonthlyRevenue
                {
                    Id = 7,
                    Month = "July",
                    Revenue = 5600
                },
                new MonthlyRevenue
                {
                    Id = 8,
                    Month = "August",
                    Revenue = 5800
                });
            builder.Entity<QuarterlyEarnings>().HasData(
                new QuarterlyEarnings
                {
                    Id = 1,
                    Quarter = "1",
                    Earnings = 3000
                },
                new QuarterlyEarnings
                {
                    Id = 2,
                    Quarter = "2",
                    Earnings = 3500
                });
            builder.Entity<OccupancyPercent>().HasData(
                new OccupancyPercent
                {
                    Id = 1,
                    Month = "January",
                    OccupancyPercentage = 0.75
                },
                new OccupancyPercent
                {
                    Id = 2,
                    Month = "February",
                    OccupancyPercentage = 0.80
                },
                new OccupancyPercent
                {
                    Id = 3,
                    Month = "March",
                    OccupancyPercentage = 0.78
                },
                new OccupancyPercent
                {
                    Id = 4,
                    Month = "April",
                    OccupancyPercentage = 0.75
                },
                new OccupancyPercent
                {
                    Id = 5,
                    Month = "May",
                    OccupancyPercentage = 0.80
                },
                new OccupancyPercent
                {
                    Id = 6,
                    Month = "June",
                    OccupancyPercentage = 0.69
                },
                new OccupancyPercent
                {
                    Id = 7,
                    Month = "July",
                    OccupancyPercentage = 0.75
                },
                new OccupancyPercent
                {
                    Id = 8,
                    Month = "August",
                    OccupancyPercentage = 0.88
                });
        }

    }
}
