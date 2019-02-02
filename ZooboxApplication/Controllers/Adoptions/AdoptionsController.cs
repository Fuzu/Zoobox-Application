using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using ZooboxApplication.Models;
using ZooboxApplication.Models.Adoptions;

namespace ZooboxApplication.Controllers.Adoptions
{
    public class AdoptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adoptions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Adoption.Include(a => a.AdoptionTypeName).Include(a => a.AnimalName).Include(a => a.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Adoptions/Details/5
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

        // GET: Adoptions/Create
        public IActionResult Create()
        {
            ViewData["AdoptionType"] = new SelectList(_context.AdoptionType, "Id", "AdoptionTypeName");
            ViewData["Animal"] = new SelectList(_context.Animal, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Email");
            return View();
        }

        // POST: Adoptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Adoptions/Edit/5
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

        // POST: Adoptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Adoptions/Delete/5
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

        // POST: Adoptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoption = await _context.Adoption.FindAsync(id);
            _context.Adoption.Remove(adoption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionExists(int id)
        {
            return _context.Adoption.Any(e => e.Id == id);
        }
    }
}
