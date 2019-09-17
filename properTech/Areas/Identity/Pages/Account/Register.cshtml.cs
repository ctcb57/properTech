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
                new SelectListItem { Value = "Manager", Text = "Manager"},
                new SelectListItem { Value = "Resident", Text = "Resident"},
                new SelectListItem { Value = "MaintenanceTech", Text = "Maintenance Tech"},
            };
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Name = Input.Name, PhoneNumber = Input.PhoneNumber, Role = Input.Role};
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
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if(user.Role == "Manager")
                    {
                        return RedirectToAction("Create", "Managers");
                    }
                    if(user.Role == "Resident")
                    {
                        return RedirectToAction("Create", "Residents");
                    }
                    if(user.Role == "MaintenanceTech")
                    {
                        return RedirectToAction("Create", "MaintenanceTeches");
                    }

                    //if (Input.isSuperAdmin)
                    //{
                    //    await _userManager.AddToRoleAsync(user, StaticDetails.SuperAdminEndUser);
                    //}
                    //else
                    //{
                    //    await _userManager.AddToRoleAsync(user, StaticDetails.Manager);
                    //}
                    //_logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Home");
        }
    }
}
