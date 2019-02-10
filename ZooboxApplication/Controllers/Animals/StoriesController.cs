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
using ZooboxApplication.Models.Animals;

namespace ZooboxApplication.Controllers.Animals
{
    class ResultJson
    {
        public int Status { get; set; }
        public string Response { get; set; }
        public object Object { get; set; }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Controlador de páginas Historias do Animal. </summary>
    ///
    /// <remarks>   André Silva, 09/12/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class StoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public StoriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Página Historias do Animal </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna a json com as Historias do Animal, com uma lista das Historias do Animal registados na Base de dados. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<JsonResult> Index()
        {
            return Json(await _context.Story.ToListAsync());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de uma Historia do Animal </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <param name="id"> - Id da Historia do Animal</param>
        /// <returns>   Retorna uma view com os detalhes de uma Historia do Animal, Caso contrário devolve Not Found</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Story
                .FirstOrDefaultAsync(m => m.Id == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de uma Historia do Animal </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna uma view com um formulário de inserção de uma Historia do Animal</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Create()
        {
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Create, endpoint para criar uma Historia do Animal recebemdo todos os argumentos necessarios. </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da historia do animal </param>
        /// <param name="Title"> - Titulo da historia do animal </param> 
        /// <param name="Description"> - Descrição da historia do animal </param> 
        /// <param name="Created"> - Data de Criação da historia do animal </param> 
        /// <param name="CreatedByID"> - Id do user que Crio da historia do animal </param>       
        /// <returns>   Retorna um json com os Detalhes da nova Historia do Animal  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Id,Title,Description,Created,CreatedByID,AnimalId")] Story story)
        {
            story.Created = DateTime.Now;
            var user = await GetCurrentUserAsync();
            story.CreatedByID = user.Id;
            if (ModelState.IsValid)
            {
                _context.Add(story);
                await _context.SaveChangesAsync();
                return Json(new ResultJson() { Status = 1, Object = story });
            }
            
            return Json(new ResultJson() { Status = 0});
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Edit, endpoint para procurar a informação e devolve de uma Historia do Animal para que se possa editar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> id da Historia do Animal </param>
        /// <returns>   Retorna um view com o formulario preenchido para editar.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Story.FindAsync(id);
            if (story == null)
            {
                return NotFound();
            }
            return View(story);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Edit, endpoint que recebe a informação relativa a uma historia do animal e edit essa Historia do Animal.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id da historia do animal </param>
        /// <param name="Title"> - Titulo da historia do animal </param> 
        /// <param name="Description"> - Descrição da historia do animal </param> 
        /// <param name="Created"> - Data de Criação da historia do animal </param> 
        /// <param name="CreatedByID"> - Id do user que Crio da historia do animal </param>       
        /// <returns>   Retorna uma view com a historia do animal editada.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Created,CreatedByID")] Story story)
        {
            if (id != story.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(story);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoryExists(story.Id))
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
            return View(story);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Delete, recebe um id de uma Historia do Animal e devolve um view com a informação do mesmo e a opção para apagar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - id da Historia do Animal </param>
        /// <returns>   Retorna uma view com a Historia do Animal.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Story
                .FirstOrDefaultAsync(m => m.Id == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Delete, recebe um id de uma Historia do Animal e apaga essa Historia do Animal.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - id da Historia do Animal </param>
        /// <returns>   Retorna o index das Historias do Animal.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var story = await _context.Story.FindAsync(id);
            _context.Story.Remove(story);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Verifica se uma Historia do Animal existe.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id">id da Historia do Animal </param>
        /// <returns>   Retorna true ou false  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool StoryExists(int id)
        {
            return _context.Story.Any(e => e.Id == id);
        }
    }
}
