using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using ZooboxApplication.Models.Animals;

namespace ZooboxApplication.Controllers.Donations
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Controlador de páginas dos Tipo de Doação. </summary>
    ///
    /// <remarks>   André Silva, 09/12/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class DonationTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationTypesController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UI.Services.IEmailSender emailSender)
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Página Tipo de Doação </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna a view da página Tipo de Doação, com uma lista dos Tipo de Doação registados na Base de dados. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Index()
        {
            return View(await _context.DonationType.ToListAsync());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de um Tipo de Doação </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <param name="Id">- Id do Tipo de Doação</param>
        /// <returns>   Retorna uma view com os detalhes de um Tipo de Doação, Caso contrário devolve Not Found</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationType = await _context.DonationType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donationType == null)
            {
                return NotFound();
            }

            return View(donationType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de um Tipo de Doação </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna uma view com um formulário de inserção de um Tipo de Doação</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Create()
        {
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Create, endpoint para criar um Tipo de Doação recebemdo todos os argumentos necessarios. </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do Tipo de Doação </param>
        /// <param name="DonationTypeName"> - Nome do Tipo de Doação </param>
        /// <returns>   Retorna um view com os Detalhes da novo Tipo de Doação  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DonationTypeName")] DonationType donationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donationType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Edit, endpoint para procurar a informação e devolve de um Tipo de Doação para que se possa editar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id">- id do Tipo de Doação </param>
        /// <returns>   Retorna um view com o formulario preenchido para editar.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationType = await _context.DonationType.FindAsync(id);
            if (donationType == null)
            {
                return NotFound();
            }
            return View(donationType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Edit, endpoint que recebe a informação relativa a um Tipo de Doação e edit esse Tipo de Doação.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id">- id do Tipo de Doação </param>
        /// <param name="DonationTypeName"> - Nome do Tipo de Doação </param>
        /// <returns>   Retorna uma view com o Tipo de Doação editado.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DonationTypeName")] DonationType donationType)
        {
            if (id != donationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationTypeExists(donationType.Id))
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
            return View(donationType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Delete, recebe um id de um Tipo de Doação e devolve um view com a informação do mesmo e a opção para apagar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id">- id do Tipo de Doação </param>
        /// <returns>   Retorna uma view com o Tipo de Doação.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donationType = await _context.DonationType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donationType == null)
            {
                return NotFound();
            }

            return View(donationType);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Delete, recebe um id de um Tipo de Doação e apaga esse Tipo de Doação.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id">- id do Tipo de Doação </param>
        /// <returns>   Retorna o index dos Tipo de Doação.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donationType = await _context.DonationType.FindAsync(id);
            _context.DonationType.Remove(donationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Verifica se um Tipo de Doação existe.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id">- id do Tipo de Doação </param>
        /// <returns>   Retorna true ou false  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool DonationTypeExists(int id)
        {
            return _context.DonationType.Any(e => e.Id == id);
        }
    }
}
