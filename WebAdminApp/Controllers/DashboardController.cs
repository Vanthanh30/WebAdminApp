// Controllers/DashboardController.cs
using Microsoft.AspNetCore.Mvc;
using WebAdminApp.Service;
using WebAdminApp.Models;
using System.Threading.Tasks;

namespace WebAdminApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var totalRecipes = await _dashboardService.GetTotalRecipes();
            var activeRecipes = await _dashboardService.GetActiveRecipes();
            var inactiveRecipes = await _dashboardService.GetInactiveRecipes();
            var totalAccounts = await _dashboardService.GetTotalAccounts();

            var model = new DashboardViewModel
            {
                TotalRecipes = totalRecipes,
                ActiveRecipes = activeRecipes,
                InactiveRecipes = inactiveRecipes,
                TotalAccounts = totalAccounts
            };

            return View(model);
        }
    }
}
