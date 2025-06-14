using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ooadepazar.Data;
using ooadepazar.Models;

namespace ooadepazar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserManagementController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: /UserManagement
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRolesViewModel.Add(new UserRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = roles
                });
            }

            return View(userRolesViewModel);
        }

        // GET: /UserManagement/EditRoles/userId
        [HttpGet]
        public async Task<IActionResult> EditRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var allRoles = await _roleManager.Roles.ToListAsync(); // Prevent open DataReader issue
            var userRoles = await _userManager.GetRolesAsync(user); // Safe async call

            var model = new EditUserRolesViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                RoleSelections = allRoles.Select(role => new RoleSelectionViewModel
                {
                    RoleName = role.Name,
                    IsSelected = userRoles.Contains(role.Name)
                }).ToList()
            };

            return View(model);
        }


        // POST: /UserManagement/EditRoles/userId
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRoles(EditUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = model.RoleSelections.Where(r => r.IsSelected).Select(r => r.RoleName);

            var rolesToAdd = selectedRoles.Except(currentRoles);
            var rolesToRemove = currentRoles.Except(selectedRoles);

            if (rolesToRemove.Contains("KurirskaSluzba"))
            {
                var narudzbe = await _context.Narudzba
                    .Where(n => n.KurirskaSluzba != null && n.KurirskaSluzba.Id == user.Id)
                    .ToListAsync();

                var defaultKurir = await _userManager.FindByIdAsync("6f9be995-61b5-4c92-838a-bd79642263ae");
                foreach (var narudzba in narudzbe)
                {
                    narudzba.KurirskaSluzba = defaultKurir;
                }
                await _context.SaveChangesAsync();
            }

            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            await _userManager.AddToRolesAsync(user, rolesToAdd);

            return RedirectToAction(nameof(Index));
        }
    }

    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class EditUserRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<RoleSelectionViewModel> RoleSelections { get; set; }
    }

    public class RoleSelectionViewModel
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
