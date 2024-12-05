using System;
namespace WebAdminApp.Models
{
    public class Account
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Tạo ID duy nhất
        public string name { get; set; }
        public string password { get; set; } // Lưu ý: Mã hóa mật khẩu trước khi lưu
        public string email { get; set; }
        public string avatar { get; set; }  // URL ảnh đại diện
    }
}

