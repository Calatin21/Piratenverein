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
    public class PiratFormersController : Controller
    {
        private readonly PiratenVereinContext _context;

        public PiratFormersController(PiratenVereinContext context)
        {
            _context = context;
        }

        // GET: PiratFormers
        public async Task<IActionResult> Index()
        {
              return _context.PiratFormers != null ? 
                          View(await _context.PiratFormers.ToListAsync()) :
                          Problem("Entity set 'PiratenVereinContext.PiratFormers'  is null.");
        }

        // GET: PiratFormers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PiratFormers == null)
            {
                return NotFound();
            }

            var piratFormer = await _context.PiratFormers
                .FirstOrDefaultAsync(m => m.Spitzname == id);
            if (piratFormer == null)
            {
                return NotFound();
            }

            return View(piratFormer);
        }

        // GET: PiratFormers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PiratFormers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Vorname,Nachname,Spitzname,Jahresalter")] PiratFormer piratFormer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(piratFormer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(piratFormer);
        }
        public async Task<IActionResult> CreateNew([Bind("Vorname,Nachname,Spitzname,Jahresalter")] PiratFormer piratFormer) {
            if (ModelState.IsValid) {
                _context.Add(piratFormer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(piratFormer);
        }
        // GET: PiratFormers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PiratFormers == null)
            {
                return NotFound();
            }

            var piratFormer = await _context.PiratFormers.FindAsync(id);
            if (piratFormer == null)
            {
                return NotFound();
            }
            return View(piratFormer);
        }

        // POST: PiratFormers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Vorname,Nachname,Spitzname,Jahresalter")] PiratFormer piratFormer)
        {
            if (id != piratFormer.Spitzname)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(piratFormer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PiratFormerExists(piratFormer.Spitzname))
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
            return View(piratFormer);
        }

        // GET: PiratFormers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PiratFormers == null)
            {
                return NotFound();
            }

            var piratFormer = await _context.PiratFormers
                .FirstOrDefaultAsync(m => m.Spitzname == id);
            if (piratFormer == null)
            {
                return NotFound();
            }

            return View(piratFormer);
        }

        // POST: PiratFormers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PiratFormers == null)
            {
                return Problem("Entity set 'PiratenVereinContext.PiratFormers'  is null.");
            }
            var piratFormer = await _context.PiratFormers.FindAsync(id);
            if (piratFormer != null)
            {
                _context.PiratFormers.Remove(piratFormer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PiratFormerExists(string id)
        {
          return (_context.PiratFormers?.Any(e => e.Spitzname == id)).GetValueOrDefault();
        }
    }
}
