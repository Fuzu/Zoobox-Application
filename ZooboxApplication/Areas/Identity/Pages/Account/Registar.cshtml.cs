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

using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Models;

namespace ZooboxApplication.Areas.Identity.Pages.Account
{
    [Authorize]
    public class RegistarModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegistarModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;




        public RegistarModel(
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
            [Display(Name = "Nome")]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Data de Nascimento")]
            public DateTime DateOfBirth { get; set; }

            [Display(Name = "Morada")]
            public string address { get; set; }

            [Required]
            [RegularExpression("^[0-9]{9}$", ErrorMessage = "Número Inválido")]
            [Display(Name = "Telefone")]
            public String PhoneNumber { get; set; }

            [Display(Name = "Informação adicional")]
            public string additionInformation { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "O {0} tem de ter pelo menos {2} e no máximo {1} número de caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirme a password")]
            [Compare("Password", ErrorMessage = "A password não está igual a confirmação da mesma.")]
            public string ConfirmPassword { get; set; }



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
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    additionInformation = Input.additionInformation,
                    address = Input.address,
                    DateOfBirth = Input.DateOfBirth,
                    Name = Input.Name
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Foi criado um novo utilizador com password");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirmar o seu email",
                        $"Por favor confirme a sua conta de email <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>ao clicar aqui</a>.");

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
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
