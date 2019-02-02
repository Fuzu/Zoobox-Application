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
    [Authorize]
    public class AdoptionTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdoptionTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdoptionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdoptionType.ToListAsync());
        }

        // GET: AdoptionTypes/Details/5
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

        // GET: AdoptionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdoptionTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: AdoptionTypes/Edit/5
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

        // POST: AdoptionTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: AdoptionTypes/Delete/5
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

        // POST: AdoptionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoptionType = await _context.AdoptionType.FindAsync(id);
            _context.AdoptionType.Remove(adoptionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionTypeExists(int id)
        {
            return _context.AdoptionType.Any(e => e.Id == id);
        }
    }
}
