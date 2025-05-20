using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Microsoft.EntityFrameworkCore;
using ooadepazar.Data;
using ooadepazar.Models;

namespace ooadepazar.Controllers
{
    public class ArtikalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtikalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Artikal
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Artikal.Include(a => a.Korisnik);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Artikal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikal = await _context.Artikal
                .Include(a => a.Korisnik)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (artikal == null)
            {
                return NotFound();
            }

            return View(artikal);
        }

        // GET: Artikal/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Set<Korisnik>(), "ID", "ID");
            return View();
        }

        // POST: Artikal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naziv,Stanje,Opis,Cijena,Lokacija,DatumObjave,KorisnikId,Korisnik")] Artikal artikal)
        {
            artikal.DatumObjave = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(artikal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Set<Korisnik>(), "ID", "ID", artikal.KorisnikId);
            return View(artikal);
        }

        // GET: Artikal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikal = await _context.Artikal.FindAsync(id);
            if (artikal == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Set<Korisnik>(), "ID", "ID", artikal.KorisnikId);
            return View(artikal);
        }

        // POST: Artikal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naziv,Stanje,Opis,Cijena,Lokacija,DatumObjave,KorisnikId")] Artikal artikal)
        {
            if (id != artikal.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artikal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtikalExists(artikal.ID))
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
            ViewData["KorisnikId"] = new SelectList(_context.Set<Korisnik>(), "ID", "ID", artikal.KorisnikId);
            return View(artikal);
        }

        // GET: Artikal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikal = await _context.Artikal
                .Include(a => a.Korisnik)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (artikal == null)
            {
                return NotFound();
            }

            return View(artikal);
        }

        // POST: Artikal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artikal = await _context.Artikal.FindAsync(id);
            if (artikal != null)
            {
                _context.Artikal.Remove(artikal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtikalExists(int id)
        {
            return _context.Artikal.Any(e => e.ID == id);
        }
    }
}
