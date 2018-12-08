using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZooboxApplication.Data;
using ZooboxApplication.Models;

namespace ZooboxApplication.Controllers
{
    public class SpeciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpeciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Species
        public async Task<IActionResult> Index()
        {
            return View(await _context.Specie.ToListAsync());
        }

        // GET: Species/Details/5
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

        // GET: Species/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Species/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Species/Edit/5
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

        // POST: Species/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Species/Delete/5
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

        // POST: Species/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var specie = await _context.Specie.FindAsync(Id);
            _context.Specie.Remove(specie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecieExists(int Id)
        {
            return _context.Specie.Any(e => e.Id == Id);
        }
    }
}
