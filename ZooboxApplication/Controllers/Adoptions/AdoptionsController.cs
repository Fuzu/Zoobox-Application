using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using ZooboxApplication.Models;
using ZooboxApplication.Models.Adoptions;

namespace ZooboxApplication.Controllers.Adoptions
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Controlador de páginas Adoções. </summary>
    ///
    /// <remarks>   André Silva, 09/12/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Authorize]
    public class AdoptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Página Adoções </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna a view da página Adoções, com uma lista das Adoções registados na Base de dados. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Adoption.Include(a => a.AdoptionTypeName).Include(a => a.AnimalName).Include(a => a.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de uma Adoção </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <param name="id"> - Id da Adoção</param>
        /// <returns>   Retorna uma view com os detalhes de uma Adoção, Caso contrário devolve Not Found</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoption
                .Include(a => a.AdoptionTypeName)
                .Include(a => a.AnimalName)
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de uma Adoção </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna uma view com um formulário de inserção de uma Adoção</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Create()
        {
            ViewData["AdoptionType"] = new SelectList(_context.AdoptionType, "Id", "AdoptionTypeName");
            ViewData["Animal"] = new SelectList(_context.Animal, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Email");
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Create, endpoint para criar uma Adoção recebemdo todos os argumentos necessarios. </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Adoção </param>
        /// <param name="AdoptionDate"> - Data da adoçao </param>
        /// <param name="Animal"> Animal que vai ser adotado</param>
        /// <param name="UserId">  utilizador a adotar o animal</param>
        /// <param name="AdoptionType">  Tipo de Adoção </param>
        /// <returns>   Retorna um view com os Detalhes da nova Adoção  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdoptionDate,Animal,UserId,AdoptionType")] Adoption adoption)
        {
            if (ModelState.IsValid )
            {
                Animal animal = _context.Animal.FirstOrDefault(a => a.Id == adoption.Animal);

                if (animal == null)
                {
                    _context.Add(adoption);
                    await _context.SaveChangesAsync();

                    if (adoption.AdoptionType == 3)
                    {
                        animal.State = 3;
                        _context.Update(animal);
                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Este animal já tem um processo de adopção.");
                    return View(adoption);
                }
            }
            ViewData["AdoptionType"] = new SelectList(_context.AdoptionType, "Id", "AdoptionTypeName", adoption.AdoptionType);
            ViewData["Animal"] = new SelectList(_context.Animal, "Id", "Name", adoption.Animal);
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Email", adoption.UserId);
            return View(adoption);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Edit, endpoint para procurar a informação e devolve de uma Adoção para que se possa editar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Adoção </param>
        /// <returns>   Retorna um view com o formulario preenchido para editar.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoption.FindAsync(id);
            if (adoption == null)
            {
                return NotFound();
            }
            ViewData["AdoptionType"] = new SelectList(_context.AdoptionType, "Id", "AdoptionTypeName", adoption.AdoptionType);
            ViewData["Animal"] = new SelectList(_context.Animal, "Id", "Name", adoption.Animal);
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Email", adoption.UserId);
            return View(adoption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Edit, endpoint que recebe a informação relativa a uma Adoção e edit essa Adoção.</summary>
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Adoção </param>
        /// <param name="AdoptionDate"> - Data da adoçao </param>
        /// <param name="Animal"> Animal que vai ser adotado</param>
        /// <param name="UserId">  utilizador a adotar o animal</param>
        /// <param name="AdoptionType">  Tipo de Adoção </param>
        /// <returns>   Retorna uma view com a Adoção editado.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdoptionDate,Animal,UserId,AdoptionType")] Adoption adoption)
        {
            if (id != adoption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoption);
                    await _context.SaveChangesAsync();

                    if (adoption.AdoptionType == 3)
                    {
                        Animal animal = _context.Animal.FirstOrDefault(a => a.Id == adoption.Animal);
                        animal.State = 3;
                        _context.Update(animal);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionExists(adoption.Id))
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
            ViewData["AdoptionType"] = new SelectList(_context.AdoptionType, "Id", "AdoptionTypeName", adoption.AdoptionType);
            ViewData["Animal"] = new SelectList(_context.Animal, "Id", "Name", adoption.Animal);
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Email", adoption.UserId);
            return View(adoption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Delete, recebe um id de uma Adoção e devolve um view com a informação do mesmo e a opção para apagar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - id da Adoção </param>
        /// <returns>   Retorna uma view com a Adoção.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoption
                .Include(a => a.AdoptionTypeName)
                .Include(a => a.AnimalName)
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Delete, recebe um id de uma Adoção e apaga essa Adoção.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - id da Adoção </param>
        /// <returns>   Retorna o index das Adoções.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoption = await _context.Adoption.FindAsync(id);
            _context.Adoption.Remove(adoption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Verifica se uma Adoção existe.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Adoção </param>
        /// <returns>   Retorna true ou false  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool AdoptionExists(int id)
        {
            return _context.Adoption.Any(e => e.Id == id);
        }
    }
}
