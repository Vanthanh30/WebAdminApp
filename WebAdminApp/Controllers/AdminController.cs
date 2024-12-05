using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAdminApp.Models;
using WebAdminApp.Service;

namespace WebAdminApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: Admin/Index
        public async Task<IActionResult> Index()
        {
            var admins = await _adminService.GetAdminsAsync();
            return View(admins);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                await _adminService.AddAdminAsync(admin);
                return RedirectToAction(nameof(Index));
            }

            return View(admin);
        }
    }
}
