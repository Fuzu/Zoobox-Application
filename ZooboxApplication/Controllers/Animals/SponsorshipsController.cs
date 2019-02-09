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
    public class SponsorshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SponsorshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sponsorships
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sponsorship.Include(s => s.Animal).Include(s => s.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sponsorships/Details/5
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

        // GET: Sponsorships/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            return View();
        }

        // POST: Sponsorships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Sponsorships/Edit/5
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

        // POST: Sponsorships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Sponsorships/Delete/5
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

        // POST: Sponsorships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sponsorship = await _context.Sponsorship.FindAsync(id);
            _context.Sponsorship.Remove(sponsorship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SponsorshipExists(int id)
        {
            return _context.Sponsorship.Any(e => e.Id == id);
        }
    }
}
