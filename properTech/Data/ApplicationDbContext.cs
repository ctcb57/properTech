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

        public DbSet<MaintenanceTech> maintenanceTeches { get; set; }
    }
}
