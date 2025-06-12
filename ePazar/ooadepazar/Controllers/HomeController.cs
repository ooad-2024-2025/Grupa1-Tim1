using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ooadepazar.Data;
using ooadepazar.Models;
using Microsoft.AspNetCore.Mvc.Rendering; // Required for SelectList and SelectListItem
using System.Linq; // Required for LINQ queries
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Markdig;
using Microsoft.Extensions.Logging; // Required for Enum

namespace ooadepazar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context; 
    }

    // This is now your main e-commerce homepage action
    public async Task<IActionResult> Index(string searchQuery, int? categoryId, string sortBy)
    {
        // --- Prepare Dropdown Options for the View ---

        // Populate Kategorija dropdown options
        var kategorije = Enum.GetValues(typeof(Kategorija))
                             .Cast<Kategorija>()
                             .Select(k => new SelectListItem
                             {
                                 Value = ((int)k).ToString(),
                                 Text = k.ToString().Replace("_", " ") // Make enum names more readable
                             })
                             .ToList();
        kategorije.Insert(0, new SelectListItem { Value = "", Text = "Sve kategorije", Selected = true }); // Add "All Categories" option
        ViewBag.KategorijaOptions = kategorije;

        // Populate Sorting dropdown options
        var sortOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "latest", Text = "Najnovije" },
            new SelectListItem { Value = "price_asc", Text = "Cijena (rastuće)" },
            new SelectListItem { Value = "price_desc", Text = "Cijena (opadajuće)" },
            new SelectListItem { Value = "name_asc", Text = "Naziv (A-Z)" }
        };
        ViewBag.SortOptions = sortOptions;

        // --- Pass Current Filter/Sort Values Back to the View (for UI state) ---
        ViewBag.CurrentSearchQuery = searchQuery;
        ViewBag.CurrentCategoryId = categoryId;
        ViewBag.CurrentSortBy = sortBy;

        // --- Fetch, Filter, and Sort Articles ---

        // Start with all articles and eager load the Korisnik (user) data
        var artikli = _context.Artikal
            .Include(a => a.Korisnik)
            .Where(a => a.Narucen == false)
            .AsQueryable();
        
        // Apply Search Filtering
        if (!string.IsNullOrEmpty(searchQuery))
        {
            artikli = artikli.Where(a => a.Naziv.Contains(searchQuery) || a.Opis.Contains(searchQuery) || a.Lokacija.Contains(searchQuery));
        }

        // Apply Category Filtering
        if (categoryId.HasValue)
        {
            artikli = artikli.Where(a => (int)a.Kategorija == categoryId.Value);
        }

        // Apply Sorting
        switch (sortBy)
        {
            case "price_asc":
                artikli = artikli.OrderBy(a => a.Cijena);
                break;
            case "price_desc":
                artikli = artikli.OrderByDescending(a => a.Cijena);
                break;
            case "name_asc":
                artikli = artikli.OrderBy(a => a.Naziv);
                break;
            case "latest": // Default sorting
            default:
                artikli = artikli.OrderByDescending(a => a.DatumObjave);
                break;
        }

        // --- Execute Query and Return to View ---

        // Convert to list and apply defensive null check for any potentially null items within the collection
        var finalArtikli = await artikli.ToListAsync();
        finalArtikli = finalArtikli.Where(item => item != null).ToList();

        return View(finalArtikli); // Pass the filtered and sorted articles to the view
    }

    public async Task<IActionResult> NE_ULAZITI_NA_OVU_RUTU_ODOSE_PARE()
    {
        return View();
    }
    
    [HttpGet("Home/GetAIResponseInMarkdown/{artikalId}")]
    public async Task<IActionResult> GetAIResponseInMarkdown(int artikalId)
    {
        var artikal = await _context.Artikal.FindAsync(artikalId);
        if (artikal == null)
        {
            return NotFound($"Artikal with ID {artikalId} not found.");
        }
        
        string prompt = $"💰{artikal.Naziv}💰\n🏘️ {artikal.Opis}\n\nCIJENA: {artikal.Cijena}, LOKACIJA: {artikal.Lokacija}";        
        OpenAIController c = new OpenAIController();
        string markdown = await c.SendMessageAsync(prompt);
        
        string html = Markdown.ToHtml(markdown);
        return Content(html, "text/html");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}