using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using ZooboxApplication.Models.Animals;

namespace ZooboxApplication.Controllers.Animals
{   
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Controlador de páginas Apadrinhamentos. </summary>
    ///
    /// <remarks>   André Silva, 09/12/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SponsorshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SponsorshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Página Apadrinhamentos </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna a view da página Apadrinhamentos, com uma lista dos Apadrinhamentos registados na Base de dados. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sponsorship.Include(s => s.Animal).Include(s => s.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de um Apadrinhamento </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <param name="id"> - Id do Apadrinhamento</param>
        /// <returns>   Retorna uma view com os detalhes de um Apadrinhamento, Caso contrário devolve Not Found</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsorship = await _context.Sponsorship
                .Include(s => s.Animal)
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sponsorship == null)
            {
                return NotFound();
            }

            return View(sponsorship);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de um Apadrinhamento </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna uma view com um formulário de inserção de um Apadrinhamento</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            return View();
        }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Create, endpoint para criar um Apadrinhamento recebemdo todos os argumentos necessarios. </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Apadrinhamento </param>
        /// <parma name="Status"> - Status dos Apadrinhamentos </param>
        /// <parma name="Title"> - Title dos Apadrinhamentos </param>
        /// <parma name="UserId"> - UserId dos Apadrinhamentos </param>
        /// <parma name="AnimalId"> - AnimalId dos Apadrinhamentos </param>     
        /// <param name="SpecieName"> - SpecieName do Apadrinhamento </param>
        /// <returns>   Retorna um view com os Detalhes da novo Apadrinhamento  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,Title,UserId,AnimalId")] Sponsorship sponsorship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sponsorship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", sponsorship.AnimalId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", sponsorship.UserId);
            return View(sponsorship);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Edit, endpoint para procurar a informação e devolve de um Apadrinhamento para que se possa editar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Apadrinhamento </param>
        /// <returns>   Retorna um view com o formulario preenchido para editar.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsorship = await _context.Sponsorship.FindAsync(id);
            if (sponsorship == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", sponsorship.AnimalId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", sponsorship.UserId);
            return View(sponsorship);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Edit, endpoint que recebe a informação relativa a um Apadrinhamento e edit desse Apadrinhamento.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Apadrinhamento </param>
        /// <parma name="Status"> - Status dos Apadrinhamentos </param>
        /// <parma name="Title"> - Title dos Apadrinhamentos </param>
        /// <parma name="UserId"> - UserId dos Apadrinhamentos </param>
        /// <parma name="AnimalId"> - AnimalId dos Apadrinhamentos </param>     
        /// <param name="SpecieName"> - SpecieName do Apadrinhamento </param>
        /// <returns>   Retorna uma view com o Apadrinhamento editado.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Title,UserId,AnimalId")] Sponsorship sponsorship)
        {
            if (id != sponsorship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponsorship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorshipExists(sponsorship.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id", sponsorship.AnimalId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", sponsorship.UserId);
            return View(sponsorship);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Delete, recebe um id de um Apadrinhamento e devolve um view com a informação do mesmo e a opção para apagar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id">- id do Apadrinhamento </param>
        /// <returns>   Retorna uma view com o Apadrinhamento.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsorship = await _context.Sponsorship
                .Include(s => s.Animal)
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sponsorship == null)
            {
                return NotFound();
            }

            return View(sponsorship);
        }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Delete, recebe um id de um Apadrinhamento e apaga desse Apadrinhamento.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - id do Apadrinhamento </param>
        /// <returns>   Retorna o index das Apadrinhamentos.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sponsorship = await _context.Sponsorship.FindAsync(id);
            _context.Sponsorship.Remove(sponsorship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Verifica se um Apadrinhamento existe.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Apadrinhamento </param>
        /// <returns>   Retorna true ou false  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
       private bool SponsorshipExists(int id)
       {
            return _context.Sponsorship.Any(e => e.Id == id);
        }
    }
}
