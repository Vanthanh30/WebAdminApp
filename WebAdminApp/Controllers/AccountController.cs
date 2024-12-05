using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAdminApp.Models;
using WebAdminApp.Service;

namespace WebAdminApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthenticationService _authService;

        public AccountController(AuthenticationService authService)
        {
            _authService = authService;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var isValidUser = await _authService.ValidateUserAsync(model);

                if (isValidUser)
                {
                    // Successful login, redirect to the Home/Index page
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    // Invalid login attempt, display error message
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }
    }
}
