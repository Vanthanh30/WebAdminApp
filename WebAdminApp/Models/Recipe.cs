namespace WebAdminApp.Models
{
    public class Recipe
    {
        public string recipeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }  // Thêm Description để lưu thông tin người tạo
        public int calories { get; set; }
        public string image { get; set; }
        public int like { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string categoryId { get; set; }
        public string userId { get; set; } 
        public string video { get; set; }
    }
}

