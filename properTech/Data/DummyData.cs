using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using properTech.Models;

namespace properTech.Data
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string adminId1 = "";

            string role1 = "Admin";
            string role2 = "Manager";
            string role3 = "Resident";
            string role4 = "MaintenanceTech";

            string password = "!23Qwe";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role1));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role2));
            }
            if (await roleManager.FindByNameAsync(role3) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role3));
            }
            if (await roleManager.FindByNameAsync(role4) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role4));
            }
            if (await userManager.FindByNameAsync("Admin1@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "Admin1@gmail.com",
                    Email = "Admin1@gmail.com",
                    FirstName = "Adam",
                    LastName = "Min",
                    isSuperAdmin = true

                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId1 = user.Id;
            }
            if (await userManager.FindByNameAsync("Manager1@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "Manager1@gmail.com",
                    Email = "Manager1@gmail.com",
                    FirstName = "Rick",
                    LastName = "Sanchez",
                    isSuperAdmin = false

                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }
        }
    }
}
