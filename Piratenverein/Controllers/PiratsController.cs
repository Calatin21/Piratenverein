using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Piratenverein.Models;

namespace Piratenverein.Controllers
{
    public class PiratsController : Controller
    {
        private readonly PiratenVereinContext _context;

        public PiratsController(PiratenVereinContext context)
        {
            _context = context;
        }

        // GET: Pirats
        public async Task<IActionResult> Index()
        {
              return _context.Pirats != null ? 
                          View(await _context.Pirats.ToListAsync()) :
                          Problem("Entity set 'PiratenVereinContext.Pirats'  is null.");
        }

        // GET: Pirats/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Pirats == null)
            {
                return NotFound();
            }

            var pirat = await _context.Pirats
                .FirstOrDefaultAsync(m => m.Spitzname == id);
            if (pirat == null)
            {
                return NotFound();
            }

            return View(pirat);
        }

        // GET: Pirats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pirats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Vorname,Nachname,Spitzname,Jahresalter")] Pirat pirat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pirat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pirat);
        }
        public async Task<IActionResult> CreateNew([Bind("Vorname,Nachname,Spitzname,Jahresalter")] Pirat pirat) {
            if (ModelState.IsValid) {
                _context.Add(pirat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pirat);
        }

        // GET: Pirats/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Pirats == null)
            {
                return NotFound();
            }

            var pirat = await _context.Pirats.FindAsync(id);
            if (pirat == null)
            {
                return NotFound();
            }
            return View(pirat);
        }

        // POST: Pirats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Vorname,Nachname,Spitzname,Jahresalter")] Pirat pirat)
        {
            if (id != pirat.Spitzname)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pirat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PiratExists(pirat.Spitzname))
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
            return View(pirat);
        }

        // GET: Pirats/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Pirats == null)
            {
                return NotFound();
            }

            var pirat = await _context.Pirats
                .FirstOrDefaultAsync(m => m.Spitzname == id);
            if (pirat == null)
            {
                return NotFound();
            }

            return View(pirat);
        }

        // POST: Pirats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Pirats == null)
            {
                return Problem("Entity set 'PiratenVereinContext.Pirats'  is null.");
            }
            var pirat = await _context.Pirats.FindAsync(id);
            if (pirat != null)
            {
                _context.Pirats.Remove(pirat);

            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("CreateNew", "PiratFormers", pirat);
        }

        private bool PiratExists(string id)
        {
          return (_context.Pirats?.Any(e => e.Spitzname == id)).GetValueOrDefault();
        }
    }
}
