using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminApp.Models;

namespace WebAdminApp.Service
{
    public class RecipeService : FirebaseService
    {
        public RecipeService(IConfiguration configuration) : base(configuration) { }

        // Lấy công thức theo trạng thái
        public async Task<List<Recipe>> GetRecipesByStatus(string status)
        {
            var recipes = await _firebaseClient.Child("Recipes").OnceAsync<Recipe>();
            return recipes
                .Where(r => r.Object.status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .Select(r =>
                {
                    var recipe = r.Object;
                    recipe.recipeId = r.Key;
                    return recipe;
                }).ToList();
        }

        // Lấy thông tin người dùng
        public async Task<Account> GetAccountById(string userId)
        {
            var account = await _firebaseClient
                .Child("Accounts")
                .Child(userId)
                .OnceSingleAsync<Account>();
            return account;
        }

        // Cập nhật trạng thái công thức
        public async Task UpdateRecipeStatus(string recipeId, string newStatus)
        {
            try
            {
                if (string.IsNullOrEmpty(recipeId) || string.IsNullOrEmpty(newStatus))
                {
                    throw new ArgumentException("recipeId hoặc newStatus không hợp lệ.");
                }

                var recipeRef = _firebaseClient.Child("Recipes").Child(recipeId);
                var recipe = await recipeRef.OnceSingleAsync<Recipe>(); // Lấy công thức
                if (recipe != null)
                {
                    recipe.status = newStatus;
                    await recipeRef.PutAsync(recipe); // Cập nhật lại công thức
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật trạng thái công thức: {ex.Message}");
                throw;
            }
        }

        // Cập nhật trạng thái cho nhiều công thức
        public async Task BulkUpdateRecipeStatus(List<string> recipeIds, string newStatus)
        {
            try
            {
                if (recipeIds == null || !recipeIds.Any() || string.IsNullOrEmpty(newStatus))
                {
                    throw new ArgumentException("recipeIds hoặc newStatus không hợp lệ.");
                }

                foreach (var recipeId in recipeIds)
                {
                    var recipeRef = _firebaseClient.Child("Recipes").Child(recipeId);
                    var recipe = await recipeRef.OnceSingleAsync<Recipe>(); // Lấy công thức
                    if (recipe != null)
                    {
                        recipe.status = newStatus;
                        await recipeRef.PutAsync(recipe); // Cập nhật lại công thức
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật trạng thái công thức: {ex.Message}");
                throw;
            }
        }
    }
}
