using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize(Roles = "Admin")] // Restreindre l'accès aux administrateurs
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var roles = new Dictionary<string, IList<string>>();

        foreach (var user in users)
            roles[user.Id] = await _userManager.GetRolesAsync(user);

        ViewBag.UserRoles = roles;

        return View(users);
    }

    public async Task<IActionResult> MakeAdmin(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
            await _userManager.AddToRoleAsync(user, "Admin");

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveAdmin(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
            await _userManager.RemoveFromRoleAsync(user, "Admin");

        return RedirectToAction(nameof(Index));
    }

    //  Supprimer un utilisateur
    public async Task<IActionResult> DeleteUser(string id)
    {
        if (id == null) return NotFound();

        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        return View(user);
    }

    [HttpPost, ActionName("DeleteUser")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUserConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            ModelState.AddModelError("", "Vous ne pouvez pas supprimer votre propre compte.");
            return RedirectToAction(nameof(Index));
        }

        await _userManager.DeleteAsync(user);

        return RedirectToAction(nameof(Index));
    }

}






    



