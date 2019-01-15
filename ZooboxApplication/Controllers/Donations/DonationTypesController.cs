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
    public class DonationTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DonationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DonationType.ToListAsync());
        }

        // GET: DonationTypes/Details/5
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

        // GET: DonationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DonationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: DonationTypes/Edit/5
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

        // POST: DonationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: DonationTypes/Delete/5
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

        // POST: DonationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donationType = await _context.DonationType.FindAsync(id);
            _context.DonationType.Remove(donationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationTypeExists(int id)
        {
            return _context.DonationType.Any(e => e.Id == id);
        }
    }
}
