using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using ZooboxApplication.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ZooboxApplication.Models;

namespace ZooboxApplication.Areas.Identity.Pages.Account
{
    [Authorize]
    public class AtribuirTipoModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegistarModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;




        public AtribuirTipoModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegistarModel> logger, RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender)
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

        public List<SelectListItem> Roles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Administrator", Text = "Administrador" },
            new SelectListItem { Value = "Employe", Text = "Funcionário" },
            new SelectListItem { Value = "Voluntary", Text = "Voluntário"  },
        };

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }



            [Display(Name = "Tipo de Utilizador")]
            public string Role { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        /// <summary>Called when [post asynchronous].</summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));


            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user != null)
                {
                    var existRole = await _roleManager.RoleExistsAsync(Input.Role);
                    if (!existRole)
                    {
                        IdentityRole role = new IdentityRole();
                        role.Name = Input.Role;
                        await _roleManager.CreateAsync(role);
                    }

                    await _userManager.AddToRoleAsync(user, Input.Role);

                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
               }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
