using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ooadepazar.Data;
using ooadepazar.Models;

namespace ooadepazar.Controllers
{
    public class NarudzbaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NarudzbaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Narudzba
        [Authorize(Roles = "KurirskaSluzba, Admin")]
        
        public async Task<IActionResult> Index()
        {
            var narudzbe = await _context.Narudzba
                .Include(n => n.Artikal)
                .Include(n => n.Korisnik)
                .Include(n => n.KurirskaSluzba)
                .ToListAsync();

            return View(narudzbe);
        }

        // GET: Narudzba/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var narudzba = await _context.Narudzba
                .Include(n => n.Artikal)
                .Include(n => n.Korisnik)
                .Include(n => n.KurirskaSluzba)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (narudzba is null)
                return NotFound();

            return View(narudzba);
        }

        [Authorize(Roles = "Admin, Korisnik")]
        public IActionResult Create(int id)
        {
            ViewBag.SelectedArtikalId = id;
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Korisnik")]
        public async Task<IActionResult> CreateNarudzba(int artikalId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var artikal = await _context.Artikal.FindAsync(artikalId);
            if (artikal == null)
            {
                return NotFound("Artikal nije pronađen.");
            }

            var narudzba = new Narudzba
            {
                DatumNarudzbe = DateTime.Now,
                DatumObrade = null,
                Status = Status.Kreiran,
                Korisnik = user,
                Artikal = artikal,
                KurirskaSluzba = null,
                Lokacija = artikal.Lokacija
            };

            _context.Narudzba.Add(narudzba);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Narudzba"); // Or wherever you want to redirect
        }


        // GET: Narudzba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var narudzba = await _context.Narudzba
                .Include(n => n.Artikal)
                .Include(n => n.Korisnik)
                .Include(n => n.KurirskaSluzba)
                .FirstOrDefaultAsync(n => n.ID == id);

            if (narudzba == null) return NotFound();

            ViewData["ArtikalID"] = new SelectList(_context.Artikal, "ID", "Naziv", narudzba.Artikal?.ID);
            ViewData["KorisnikID"] = new SelectList(_context.Users, "Id", "Ime", narudzba.Korisnik?.Id);
            ViewData["KurirskaSluzbaID"] = new SelectList(_context.Users, "Id", "Ime", narudzba.KurirskaSluzba?.Id);
            return View(narudzba);
        }

        // POST: Narudzba/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DatumNarudzbe,DatumObrade,Status")] Narudzba narudzba, string korisnikId, string kurirskaSluzbaId, int artikalId)
        {
            if (id != narudzba.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    narudzba.Korisnik = await _context.Users.FindAsync(korisnikId);
                    narudzba.KurirskaSluzba = await _context.Users.FindAsync(kurirskaSluzbaId);
                    narudzba.Artikal = await _context.Artikal.FindAsync(artikalId);

                    _context.Update(narudzba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudzbaExists(narudzba.ID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ArtikalID"] = new SelectList(_context.Artikal, "ID", "Naziv", artikalId);
            ViewData["KorisnikID"] = new SelectList(_context.Users, "Id", "Ime", korisnikId);
            ViewData["KurirskaSluzbaID"] = new SelectList(_context.Users, "Id", "Ime", kurirskaSluzbaId);
            return View(narudzba);
        }


        // POST: Narudzba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var narudzba = await _context.Narudzba.FindAsync(id);
            if (narudzba != null)
            {
                _context.Narudzba.Remove(narudzba);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarudzbaExists(int id)
        {
            return _context.Narudzba.Any(e => e.ID == id);
        }
    }
}
