using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using ZooboxApplication.Models.Adoptions;

namespace ZooboxApplication.Controllers.Adoptions
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Controlador de páginas Tipos de Adoção. </summary>
    ///
    /// <remarks>   André Silva, 09/12/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Authorize]
    public class AdoptionTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Página Tipos de Adoção </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna a view da página Tipos de Adoção, com uma lista das Tipos de Adoção registados na Base de dados. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdoptionType.ToListAsync());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de um Tipo de Adoção </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <param name="Id">- Id do Tipo de Adoção</param>
        /// <returns>   Retorna uma view com os detalhes de um Tipo de Adoção, Caso contrário devolve Not Found</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionType = await _context.AdoptionType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionType == null)
            {
                return NotFound();
            }

            return View(adoptionType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de um Tipo de Adoção </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna uma view com um formulário de inserção de um Tipo de Adoção</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Create()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Create, endpoint para criar um Tipo de Adoção recebemdo todos os argumentos necessarios. </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Tipo de Adoção </param>
        /// <param name="AdoptionTypeName"> - Nome do Tipo de Adoção </param>
        /// <returns>   Retorna um view com os Detalhes da novo Tipo de Adoção  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdoptionTypeName")] AdoptionType adoptionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adoptionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adoptionType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Edit, endpoint para procurar a informação e devolve de um Tipo de Adoção para que se possa editar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
       /// <param name="Id"> - id do Tipo de Adoção </param>
        /// <returns>   Retorna um view com o formulario preenchido para editar.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionType = await _context.AdoptionType.FindAsync(id);
            if (adoptionType == null)
            {
                return NotFound();
            }
            return View(adoptionType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Edit, endpoint que recebe a informação relativa a um Tipo de Adoção e edit esso Tipo de Adoção.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Tipo de Adoção </param>
        /// <param name="AdoptionTypeName"> - Nome do Tipo de Adoção </param>
        /// <returns>   Retorna uma view com o Tipo de Adoção editado.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdoptionTypeName")] AdoptionType adoptionType)
        {
            if (id != adoptionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoptionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionTypeExists(adoptionType.Id))
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
            return View(adoptionType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Delete, recebe um id de um Tipo de Adoção e devolve um view com a informação do mesmo e a opção para apagar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Tipo de Adoção </param>
        /// <returns>   Retorna uma view com o Tipo de Adoção.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionType = await _context.AdoptionType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionType == null)
            {
                return NotFound();
            }

            return View(adoptionType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Delete, recebe um id de um Tipo de Adoção e apaga esso Tipo de Adoção.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
         /// <param name="Id"> - id do Tipo de Adoção </param>
        /// <returns>   Retorna o index dos Tipos de Adoção.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoptionType = await _context.AdoptionType.FindAsync(id);
            _context.AdoptionType.Remove(adoptionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Verifica se um Tipo de Adoção existe.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Tipo de Adoção </param>
        /// <returns>   Retorna true ou false  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool AdoptionTypeExists(int id)
        {
            return _context.AdoptionType.Any(e => e.Id == id);
        }
    }
}
