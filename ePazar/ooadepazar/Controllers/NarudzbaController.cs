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
        private readonly IMailService _mailService;

        public NarudzbaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMailService mailService) {
            _context = context;
            _userManager = userManager;
            _mailService = mailService;
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

        /*
        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.SelectedArtikalId = id;
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Ime = user?.Ime ?? "";
            ViewBag.Prezime = user?.Prezime ?? "";
            ViewBag.Telefon = user?.BrojTelefona ?? "";
            ViewBag.Lokacija = user?.Adresa ?? "";
            return View();
        }
        */

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            var artikal = await _context.Artikal.FirstOrDefaultAsync(a => a.ID == id);
            if (artikal == null) return NotFound();

            var korisnik = await _userManager.GetUserAsync(User);
            if (korisnik == null) return Unauthorized();

            ViewBag.ArtikalId = id;
            ViewBag.Korisnik = korisnik;
            return View();
        }


        // POST
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public IActionResult Create(int id, Narudzba narudzba)
        {
            var artikal = _context.Artikal.FirstOrDefault(a => a.ID == id);
            if (artikal == null)
                return NotFound();

            narudzba.DatumNarudzbe = DateTime.Now;
            narudzba.Status = Status.Kreiran; // Postavi početni status
            narudzba.Artikal = artikal;
            narudzba.Korisnik = _userManager.GetUserAsync(User).Result; // Postavi korisnika na trenutnog prijavljenog

            _context.Narudzba.Add(narudzba);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(int id, string Lokacija)
        {
            var korisnik = await _userManager.GetUserAsync(User);
            if (korisnik == null) return Unauthorized();

            var artikal = await _context.Artikal.FirstOrDefaultAsync(a => a.ID == id);
            if (artikal == null) return NotFound();

            var narudzba = new Narudzba
            {
                DatumNarudzbe = DateTime.Now,
                Status = Status.Kreiran,
                Lokacija = Lokacija,
                Artikal = artikal,
                Korisnik = korisnik
            };

            _context.Narudzba.Add(narudzba);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "KurirskaSluzba, Admin")]
        public async Task<IActionResult> PromijeniStatus(int id, string noviStatus) {
            var narudzba = await _context.Narudzba
                .Include(n => n.Korisnik)
                .Include(n => n.Artikal)
                .FirstOrDefaultAsync(n => n.ID == id);

            if (narudzba == null)
                return NotFound();

            // Parse noviStatus to enum
            if (!Enum.TryParse<Status>(noviStatus, out var status))
                return BadRequest();

            narudzba.Status = status;

            // Set DatumObrade if finished
            if (status == Status.Dostavljen)
                narudzba.DatumObrade = DateTime.Now;

            await _context.SaveChangesAsync();

            // Send email notification
            if (narudzba.Korisnik != null) {
                string subject = "Status vaše narudžbe je promijenjen";
                string message = status switch {
                    Status.UObradi => $"Vaša narudžba za artikal \"{narudzba.Artikal?.Naziv}\" je sada u obradi.",
                    Status.Dostavljen => $"Vaša narudžba za artikal \"{narudzba.Artikal?.Naziv}\" je završena i označena kao dostavljena.",
                    _ => $"Status vaše narudžbe je promijenjen u: {status}."
                };
                await _mailService.SendEmailAsync(narudzba.Korisnik.EmailAdresa, message);

            }

            return RedirectToAction(nameof(Index));
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
