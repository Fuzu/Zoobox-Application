using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using ZooboxApplication.Models;
using ZooboxApplication.Models.ViewModel;

namespace ZooboxApplication.Controllers.Animals
{
   
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Controlador de páginas Animais. </summary>
    ///
    /// <remarks>   André Silva, 09/12/2018. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Authorize]
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IViewRenderService _viewRenderService;
        private readonly IHostingEnvironment _env;
        public AnimalsController(ApplicationDbContext context, IHostingEnvironment env, IViewRenderService viewRenderService)
        {
            _context = context;
            _env = env;

            _viewRenderService = viewRenderService;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Página Animais </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna a view da página Animais, com uma lista dos animais registados na Base de dados. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Index(string searchString, string breed)
        {
            //var applicationDbContext = _context.Animal.Include(a => a.DiseaseName).Include(a => a.RaceName).Include(a => a.Statename);
            var animals = from m in _context.Animal.Include(a => a.DiseaseName)
                .Include(a => a.RaceName)
                .Include(a => a.Statename)
                          select m;

            var raceList = from r in _context.Race
                           orderby r.RaceName
                           select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                animals = animals.Where(s => s.Name.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(breed))
            {
                animals = animals.Where(s => s.RaceName.Id.ToString() == breed);
            }
            var viewModel = new AnimalViewModelIndex();
            viewModel.AnimalList = await animals.ToListAsync();
            var temList = new List<Race>(await raceList.ToListAsync());
            viewModel.Race = temList.ConvertAll(item => new SelectListItem(
                text: item.RaceName,
                value: item.Id.ToString()
                )
            );
            return View(viewModel);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de um Animal </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <param name="id"> - Id do Animal</param>
        /// <returns>   Retorna uma view com os detalhes de um Animal, Caso contrário devolve Not Found</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.DiseaseName)
                .Include(a => a.RaceName)
                .Include(a => a.Statename)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Procura todas as stories de um animal e devolve.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - Id do Animal</param>
        /// <returns>   Retorna lista de stories  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<JsonResult> Stories(int? id)
        {
            if (id == null)
            {
                return Json(new ResultJson() { Status = 0 });
            }

            var animal = await _context.Animal.FirstOrDefaultAsync(m => m.Id == id);
            animal.Stories = _context.Story.Where(a => a.AnimalId == animal.Id).OrderByDescending(a => a.Created).ToList();
            if (animal == null)
            {
                return Json(new ResultJson() { Status = 0 });
            }

            animal.RaceName = _context.Race.Find(animal.Race);
            animal.Statename = _context.State.Find(animal.State);
            var view = new string("");
            if (_viewRenderService != null)
            {
                 view = await _viewRenderService.RenderToStringAsync("Animals/Stories", animal);
            }
            
            

            return Json(new ResultJson() { Status = 1 , Object = view });
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detalhes de um Animal </summary>
        ///
        /// <remarks>   André Silva, 09/12/2018. </remarks>
        ///
        /// <returns>   Retorna uma view com um formulário de inserção de um animal</returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Create()
        {
            ViewData["Disease"] = new SelectList(_context.DiseaseAnimal, "Id", "DiseaseName");
            ViewData["Race"] = new SelectList(_context.Race, "Id", "RaceName");
            ViewData["State"] = new SelectList(_context.State, "Id", "StateName");
            return View();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Create, endpoint para criar um Animal recebemdo todos os argumentos necessarios. </summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do animal </param>
        /// <param name="Name"> - Name do animal </param>
        /// <param name="Race"> - Race do animal </param>
        /// <param name="Disease"> - Disease do animal </param>
        /// <param name="EntranceDay"> - EntranceDay do animal </param>
        /// <param name="Location"> - Location do animal </param>
        /// <param name="State"> - State do animal </param>
        /// <param name="Image"> - Image do animal </param>
        /// <returns>   Retorna um view com os Detalhes do novo Animal  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Race,Disease,EntranceDay,Location,State,Image")] Animal animal)
        {
            if(animal.Image != null && _env != null)
            {
                string path = Path.Combine(_env.WebRootPath, "images/upload/"+animal.Image.FileName);
               
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    animal.Image.CopyTo(fs);
                }
                animal.ImageFile = "/images/upload/"+animal.Image.FileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Disease"] = new SelectList(_context.DiseaseAnimal, "Id", "Id", animal.Disease);
            ViewData["Race"] = new SelectList(_context.Race, "Id", "Id", animal.Race);
            ViewData["State"] = new SelectList(_context.State, "Id", "Id", animal.State);
            return View(animal);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Edit, endpoint para procurar a informação e devolve de um Animal para que se possa editar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do animal </param>
        /// <returns>   Retorna um view com o formulario preenchido para editar.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["Disease"] = new SelectList(_context.DiseaseAnimal, "Id", "DiseaseName", animal.Disease);
            ViewData["Race"] = new SelectList(_context.Race, "Id", "RaceName", animal.Race);
            ViewData["State"] = new SelectList(_context.State, "Id", "StateName", animal.State);
            animal.Stories = _context.Story.Where(a => a.AnimalId == animal.Id).OrderByDescending(a => a.Created).ToList();
            return View(animal);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> POST:Edit, endpoint que recebe a informação relativa a um Animal e edit esse Animal.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="Id"> - id do animal </param>
        /// <param name="Name"> - Name do animal </param>
        /// <param name="Race"> - Race do animal </param>
        /// <param name="Disease"> - Disease do animal </param>
        /// <param name="EntranceDay"> - EntranceDay do animal </param>
        /// <param name="Location"> - Location do animal </param>
        /// <param name="State"> - State do animal </param>
        /// <param name="Image"> - Image do animal </param>
        /// <returns>   Retorna uma view com o Animal editado.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Race,Disease,EntranceDay,Location,State,Image")] Animal animal)
        {


            if (id != animal.Id)
            {
                return NotFound();
            }

            if (animal.Image != null && _env != null)
            {
                string path = Path.Combine(_env.WebRootPath, "images/upload/" + animal.Image.FileName);

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    animal.Image.CopyTo(fs);
                }
                animal.ImageFile = "/images/upload/" + animal.Image.FileName;
            }

            if (ModelState.IsValid)
            {
                try
                {

                    //Verificar se o animal foi associado com alguma doença, se sim alterar o seu estado actual para Doente
                    if (animal.Disease != 1)
                        animal.State = 6;
                    //caso contrário colocar o estado do animal como Saudável
                    else if (animal.Disease == 1)
                        animal.State = 1;



                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
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
            ViewData["Disease"] = new SelectList(_context.DiseaseAnimal, "Id", "Id", animal.Disease);
            ViewData["Race"] = new SelectList(_context.Race, "Id", "Id", animal.Race);
            ViewData["State"] = new SelectList(_context.State, "Id", "Id", animal.State);
            return View(animal);
        }

       ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> GET:Delete, recebe um id de um Animal e devolve um view com a informação do mesmo e a opção para apagar.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - id do animal </param>
        /// <returns>   Retorna uma view com o Animal.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animal
                .Include(a => a.DiseaseName)
                .Include(a => a.RaceName)
                .Include(a => a.Statename)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Delete, recebe um id de um Animal e apaga esse Animal.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - id do animal </param>
        /// <returns>   Retorna o index dos Animals.  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary> Verifica se um animal existe.</summary>
        ///
        /// <remarks>   Tiago Alves, 10/01/2019. </remarks>
        /// <param name="id"> - id do animal </param>
        /// <returns>   Retorna true ou false  </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.Id == id);
        }
    }
}
