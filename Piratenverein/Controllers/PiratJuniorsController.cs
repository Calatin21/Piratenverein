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
    public class PiratJuniorsController : Controller
    {
        private readonly PiratenVereinContext _context;

        public PiratJuniorsController(PiratenVereinContext context)
        {
            _context = context;
        }

        // GET: PiratJuniors
        public async Task<IActionResult> Index()
        {
              return _context.PiratJuniors != null ? 
                          View(await _context.PiratJuniors.ToListAsync()) :
                          Problem("Entity set 'PiratenVereinContext.PiratJuniors'  is null.");
        }

        // GET: PiratJuniors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PiratJuniors == null)
            {
                return NotFound();
            }

            var piratJunior = await _context.PiratJuniors
                .FirstOrDefaultAsync(m => m.Spitzname == id);
            if (piratJunior == null)
            {
                return NotFound();
            }

            return View(piratJunior);
        }

        // GET: PiratJuniors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PiratJuniors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Vorname,Nachname,Spitzname,Jahresalter")] PiratJunior piratJunior)
        {
            if (ModelState.IsValid)
            {
                _context.Add(piratJunior);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(piratJunior);
        }
        public async Task<IActionResult> CreateNew([Bind("Vorname,Nachname,Spitzname,Jahresalter")] PiratJunior piratJunior) {
            if (ModelState.IsValid) {
                _context.Add(piratJunior);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(piratJunior);
        }
        // GET: PiratJuniors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PiratJuniors == null)
            {
                return NotFound();
            }

            var piratJunior = await _context.PiratJuniors.FindAsync(id);
            if (piratJunior == null)
            {
                return NotFound();
            }
            return View(piratJunior);
        }

        // POST: PiratJuniors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Vorname,Nachname,Spitzname,Jahresalter")] PiratJunior piratJunior)
        {
            if (id != piratJunior.Spitzname)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(piratJunior);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PiratJuniorExists(piratJunior.Spitzname))
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
            return View(piratJunior);
        }

        // GET: PiratJuniors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PiratJuniors == null)
            {
                return NotFound();
            }

            var piratJunior = await _context.PiratJuniors
                .FirstOrDefaultAsync(m => m.Spitzname == id);
            if (piratJunior == null)
            {
                return NotFound();
            }

            return View(piratJunior);
        }

        // POST: PiratJuniors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PiratJuniors == null)
            {
                return Problem("Entity set 'PiratenVereinContext.PiratJuniors'  is null.");
            }
            var piratJunior = await _context.PiratJuniors.FindAsync(id);
            if (piratJunior != null)
            {
                _context.PiratJuniors.Remove(piratJunior);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PiratJuniorExists(string id)
        {
          return (_context.PiratJuniors?.Any(e => e.Spitzname == id)).GetValueOrDefault();
        }
        public IActionResult LetzteGeneration() {
            foreach (PiratJunior item in _context.PiratJuniors) {
                Pirat neu = new();
                neu.Vorname = item.Vorname;
                neu.Nachname = item.Nachname;
                neu.Spitzname = item.Spitzname;
                neu.Jahresalter = item.Jahresalter;
                _context.Pirats.Add(neu);
                _context.PiratJuniors.Remove(item); }
            return RedirectToAction("Index", "Pirats"); }
    }
}
