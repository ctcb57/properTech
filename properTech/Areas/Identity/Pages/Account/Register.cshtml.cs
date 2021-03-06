﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using properTech.Models;
using properTech.Utility;

namespace properTech.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public List<SelectListItem> UserRoles { get; private set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Super Admin")]
            public bool isSuperAdmin { get; set; }

            [Required]
            public string Role { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            UserRoles = new List<SelectListItem>()
            {
                //new SelectListItem { Value = "Manager", Text = "Manager"},
                //new SelectListItem { Value = "Resident", Text = "Resident"},
                //new SelectListItem { Value = "Maintenance", Text = "Maintenance"},
                new SelectListItem { Value = "UnassignedUser", Text = "UnassignedUser"},
            };
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Role = Input.Role,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if(!await _roleManager.RoleExistsAsync(StaticDetails.AdminEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.AdminEndUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.SuperAdminEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.SuperAdminEndUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.Manager))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.Manager));
                    }
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.Maintenance))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.Maintenance));
                    }
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.Resident))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.Resident));
                    }
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.UnassignedUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticDetails.UnassignedUser));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //if (user.Role == "Manager")
                    //{
                    //    await _userManager.AddToRoleAsync(user, StaticDetails.Manager);
                    //    var role = await _userManager.GetRolesAsync(user);
                    //    return RedirectToAction("Create", "Managers", new { id = user.Id });
                    //}
                    //if (user.Role == "Resident")
                    //{
                    //    await _userManager.AddToRoleAsync(user, StaticDetails.Resident);
                    //    return RedirectToAction("Create", "Residents", new { id = user.Id });
                    //}
                    //if(user.Role == "Maintenance")
                    //{
                    //    await _userManager.AddToRoleAsync(user, StaticDetails.Maintenance);
                    //    return RedirectToAction("Create", "MaintenanceTechs", new { id = user.Id });
                    //}
                    if(user.Role == "UnassignedUser")
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.UnassignedUser);
                        var role = await _userManager.GetRolesAsync(user);
                        return RedirectToAction("UserIndex", "Home", new { id = user.Id });
                    }
                }

            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Home");
        }
    }
}
