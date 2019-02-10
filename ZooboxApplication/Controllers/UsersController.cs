using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using ZooboxApplication.Models;
using ZooboxApplication.Models.ViewModel;

namespace ZooboxApplication.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Controlador de páginas Utilizadores. </summary>
    ///
    /// <remarks>   André Silva, 09/12/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

    
        public List<SelectListItem> Roles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Administrator", Text = "Administrador" },
            new SelectListItem { Value = "Employe", Text = "Funcionário" },
            new SelectListItem { Value = "Voluntary", Text = "Voluntário"  },
        };


        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            ApplicationSeed seed = new ApplicationSeed(context, userManager, roleManager);
            seed.Users().Wait();
            seed.Animals().Wait();
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Págino Utilizadors </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna a view da págino Utilizadors, com uma lista dos Utilizadores registados na Base de dados. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Index()
        {
            var roles = from m in _context.UserRoles
                          select m;

            var allusers = _context.Users.ToList();


            //var admins = allusers.Where(x => x.Role.Select(role => role.ToString()).Contains("Admin")).ToList();
            //var adminsVM = admins.Select(user => new UserViewModel { Username = user.FullName, Roles = string.Join(",", user.Roles.Select(role => role.Name)) }).ToList();
            //var model = new GroupedUserViewModel { Users = userVM, Admins = adminsVM };


            return View(allusers);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Edit, endpoint para procurar a informação e devolve de um Utilizador para que se possa editar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Utilizador </param>
        /// <returns>   Retorna um view com o formulario preenchido para editar.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Edit(string id)
        {

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
           // ViewData["UserId"] = new SelectList(_context.Set<ApplicationUser>(), "Id", "Email", job.UserId);
            return View(user);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Edit, endpoint que recebe a informação relativa a um Utilizador e edit esso Utilizador.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Utilizador </param>
        /// <param name="Name"> Nome do utilizador </param>
        /// <param name="Email"> Email do utilizador </param>
        /// <param name="DateOfBirth"> Data de Nascimento </param>
        /// <param name="address"> Morada do utilizador </param>
        /// <param name="PhoneNumber"> Numero de telefone </param>
        /// <param name="additionInformation"> Informaçao adiciona do utilizador </param>
        /// <param name="Role"> Role do utilizador </param> 
        /// <returns>   Retorna uma view com o Utilizador editado.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(String id, [Bind("Id,Name,Email,DateOfBirth,address,PhoneNumber,additionInformation,Role")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userBD = await _userManager.FindByIdAsync(id);

                    userBD.Name = user.Name;
                    userBD.Email = user.Email;
                    userBD.DateOfBirth = user.DateOfBirth;
                    userBD.address = user.address;

                    userBD.PhoneNumber = user.PhoneNumber;
                    userBD.additionInformation = user.additionInformation;
                    userBD.Role = user.Role;


                    _context.Update(userBD);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailExists(user.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Delete, recebe um id de um Utilizador e devolve um view com a informação do mesmo e a opção para apagar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - id do Utilizador </param>
        /// <returns>   Retorna uma view com o Utilizador.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Delete(string id)
        {
            if (id == "")
            {
                return NotFound();
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Verifica se um Utilizador existe.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Utilizador </param>
        /// <returns>   Retorna true ou false  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
  
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Valida se um email ja existe na plataforma </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        ///
        /// <param name="email" - Email a ser verificado</param>
        /// <returns>   True or False</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool EmailExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de um User</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        ///
        /// <param name="id"> - Id do Utilizador</param>
        /// <returns>   Retorna uma view com os detalhes de um Utilizador, Caso contrário devolve Not Found</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);



            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}