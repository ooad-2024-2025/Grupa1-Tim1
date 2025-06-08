using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ooadepazar.Models;

namespace ooadepazar.Controllers;

public class KorisnikController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public KorisnikController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [Route("Korisnik/{id}")]
    public async Task<IActionResult> Index(string id)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();

        return View(user);
    }
}