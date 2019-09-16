using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using properTech.Models;

namespace properTech.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Manager> Manager { get; set; }

        public DbSet<Resident> Resident { get; set; }

        public DbSet<MaintenanceTech> maintenanceTeches { get; set; }
    }
}
