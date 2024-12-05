using Firebase.Database;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminApp.Models;

namespace WebAdminApp.Service
{
    public class DashboardService : FirebaseService
    {
        public DashboardService(IConfiguration configuration) : base(configuration)
        {
        }

        // Get total number of recipes
        public async Task<int> GetTotalRecipes()
        {
            var recipes = await _firebaseClient.Child("Recipes").OnceAsync<Recipe>();
            return recipes.Count();
        }

        // Get total number of active recipes
        public async Task<int> GetActiveRecipes()
        {
            var recipes = await _firebaseClient.Child("Recipes").OnceAsync<Recipe>();
            return recipes.Count(r => r.Object.status == "active");
        }

        // Get total number of inactive recipes
        public async Task<int> GetInactiveRecipes()
        {
            var recipes = await _firebaseClient.Child("Recipes").OnceAsync<Recipe>();
            return recipes.Count(r => r.Object.status == "inactive");
        }

        // Get total number of user accounts
        public async Task<int> GetTotalAccounts()
        {
            var userAccounts = await _firebaseClient.Child("Accounts").OnceAsync<Account>();
            return userAccounts.Count();
        }

        // Fetch all recipes (can be used for other purposes)
        public async Task<List<Recipe>> GetAllRecipes()
        {
            var response = await _firebaseClient.Child("Recipes").OnceAsync<Recipe>();
            return response.Select(item =>
            {
                var recipe = item.Object;
                recipe.recipeId = item.Key; // Ensure each recipe has a unique ID
                return recipe;
            }).ToList();
        }

        // Fetch all user accounts (can be used for other purposes)
        public async Task<List<Account>> GetAllUserAccounts()
        {
            var response = await _firebaseClient.Child("Accounts").OnceAsync<Account>();
            return response.Select(item =>
            {
                var account = item.Object;
                account.Id = item.Key; // Ensure each account has a unique ID
                return account;
            }).ToList();
        }
    }
}
