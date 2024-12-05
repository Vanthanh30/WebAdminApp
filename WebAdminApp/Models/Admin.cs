using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdminApp.Models
{
    public class Admin
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Tạo ID duy nhất
        public string Username { get; set; }
        public string Password { get; set; } // Lưu ý: Mã hóa mật khẩu trước khi lưu
        public string Email { get; set; }
    }
}

