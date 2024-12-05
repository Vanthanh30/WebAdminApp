using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminApp.Models;
using WebAdminApp.Service;

namespace WebAdminApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeService _recipeService;

        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // Hiển thị danh sách công thức cần duyệt (inactive)
        public async Task<IActionResult> PendingApproval()
        {
            var recipes = await _recipeService.GetRecipesByStatus("inactive");
            foreach (var recipe in recipes)
            {
                // Lấy thông tin người tạo dựa trên UserId
                var creator = await _recipeService.GetAccountById(recipe.userId);
                recipe.description = $" {creator?.name ?? "Không xác định"}"; // Gắn tên người tạo
            }
            return View(recipes);
        }

        // Hiển thị danh sách công thức đã duyệt (active)
        public async Task<IActionResult> ApprovedRecipes()
        {
            var recipes = await _recipeService.GetRecipesByStatus("active");
            foreach (var recipe in recipes)
            {
                // Lấy thông tin người tạo dựa trên UserId
                var creator = await _recipeService.GetAccountById(recipe.userId);
                recipe.description = $" {creator?.name ?? "Không xác định"}"; // Gắn tên người tạo
            }
            return View(recipes);
        }

        // Duyệt nhiều công thức
        [HttpPost]
        public async Task<IActionResult> BulkApproveRecipes(List<string> selectedRecipes)
        {
            if (selectedRecipes == null || !selectedRecipes.Any())
            {
                ViewData["Error"] = "Chưa chọn công thức nào để duyệt.";
                return RedirectToAction("PendingApproval");
            }

            // Duyệt tất cả công thức đã chọn
            foreach (var recipeId in selectedRecipes)
            {
                await _recipeService.UpdateRecipeStatus(recipeId, "active"); // Đổi trạng thái sang 'active'
            }

            // Chuyển hướng đến trang danh sách công thức đã duyệt
            return RedirectToAction("Approved");
        }
        public async Task<IActionResult> Approved()
        {
            var recipes = await _recipeService.GetRecipesByStatus("active");
            foreach (var recipe in recipes)
            {
                // Lấy thông tin người tạo dựa trên UserId
                var creator = await _recipeService.GetAccountById(recipe.userId);
                recipe.description = $" {creator?.name ?? "Không xác định"}"; // Gắn tên người tạo
            }
            return View(recipes);
        }
        // Từ chối công thức
        [HttpPost]
        public async Task<IActionResult> RejectRecipe(string recipeId)
        {
            if (string.IsNullOrEmpty(recipeId))
            {
                ViewData["Error"] = "Công thức không hợp lệ.";
                return RedirectToAction("PendingApproval");
            }

            await _recipeService.UpdateRecipeStatus(recipeId, "inactive"); // Đổi trạng thái sang 'inactive'
            return RedirectToAction("PendingApproval");
        }
    }
}
