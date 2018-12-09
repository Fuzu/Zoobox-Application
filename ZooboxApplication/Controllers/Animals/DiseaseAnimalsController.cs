using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using ZooboxApplication.Models.Animals;

namespace ZooboxApplication.Controllers.Animals
{
    [Authorize]
    public class DiseaseAnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiseaseAnimalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiseaseAnimals
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiseaseAnimal.ToListAsync());
        }

        // GET: DiseaseAnimals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseAnimal = await _context.DiseaseAnimal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diseaseAnimal == null)
            {
                return NotFound();
            }

            return View(diseaseAnimal);
        }

        // GET: DiseaseAnimals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiseaseAnimals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DiseaseName")] DiseaseAnimal diseaseAnimal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diseaseAnimal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diseaseAnimal);
        }

        // GET: DiseaseAnimals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseAnimal = await _context.DiseaseAnimal.FindAsync(id);
            if (diseaseAnimal == null)
            {
                return NotFound();
            }
            return View(diseaseAnimal);
        }

        // POST: DiseaseAnimals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DiseaseName")] DiseaseAnimal diseaseAnimal)
        {
            if (id != diseaseAnimal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diseaseAnimal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseaseAnimalExists(diseaseAnimal.Id))
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
            return View(diseaseAnimal);
        }

        // GET: DiseaseAnimals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diseaseAnimal = await _context.DiseaseAnimal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diseaseAnimal == null)
            {
                return NotFound();
            }

            return View(diseaseAnimal);
        }

        // POST: DiseaseAnimals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diseaseAnimal = await _context.DiseaseAnimal.FindAsync(id);
            _context.DiseaseAnimal.Remove(diseaseAnimal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseaseAnimalExists(int id)
        {
            return _context.DiseaseAnimal.Any(e => e.Id == id);
        }
    }
}
