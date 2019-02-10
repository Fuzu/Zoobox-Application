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

namespace ZooboxApplication.Controllers
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Controlador de páginas Espécies. </summary>
    ///
    /// <remarks>   André Silva, 09/12/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Authorize]
    public class SpeciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpeciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Página Espécies </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna a view da página Espécies, com uma lista das Espécies registados na Base de dados. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            return View(await _context.Specie.ToListAsync());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de uma Espécie </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <param name="id"> - Id da Espécie</param>
        /// <returns>   Retorna uma view com os detalhes de uma Espécie, Caso contrário devolve Not Found</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var specie = await _context.Specie
                .FirstOrDefaultAsync(m => m.Id == Id);
            if (specie == null)
            {
                return NotFound();
            }

            return View(specie);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de uma Espécie </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna uma view com um formulário de inserção de uma Espécie</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Create()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Create, endpoint para criar uma Espécie recebemdo todos os argumentos necessarios. </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - Id da Espécie</param>
        /// <param name="SpecieName"> - Nome da Espécie </param>
        /// <returns>   Retorna um view com os Detalhes da nova Espécie  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SpecieName")] Specie specie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specie);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Edit, endpoint para procurar a informação e devolve de uma Espécie para que se possa editar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - Id da Espécie</param>
        /// <returns>   Retorna um view com o formulario preenchido para editar.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var specie = await _context.Specie.FindAsync(Id);
            if (specie == null)
            {
                return NotFound();
            }
            return View(specie);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Edit, endpoint que recebe a informação relativa a uma Espécie e edit essa Espécie.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - Id da Espécie</param>
        /// <param name="SpecieName"> - Nome da Espécie </param>
        /// <returns>   Retorna uma view com a Espécie editado.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,SpecieName")] Specie specie)
        {
            if (Id != specie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecieExists(specie.Id))
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
            return View(specie);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Delete, recebe um id de uma Espécie e devolve um view com a informação do mesmo e a opção para apagar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - Id da Espécie</param>
        /// <returns>   Retorna uma view com a Espécie.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var specie = await _context.Specie
                .FirstOrDefaultAsync(m => m.Id == Id);
            if (specie == null)
            {
                return NotFound();
            }

            return View(specie);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Delete, recebe um id de uma Espécie e apaga essa Espécie.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Espécie </param>
        /// <returns>   Retorna o index das Espécies.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var specie = await _context.Specie.FindAsync(Id);
            _context.Specie.Remove(specie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Verifica se uma Espécie existe.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da Espécie </param>
        /// <returns>   Retorna true ou false  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool SpecieExists(int Id)
        {
            return _context.Specie.Any(e => e.Id == Id);
        }
    }
}
