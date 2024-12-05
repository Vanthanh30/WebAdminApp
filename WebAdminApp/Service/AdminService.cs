using Firebase.Database.Query;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdminApp.Models;

namespace WebAdminApp.Service
{
    public class AdminService : FirebaseService
    {
        public AdminService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task AddAdminAsync(Admin admin)
        {
            // Mã hóa mật khẩu trước khi lưu
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);

            await _firebaseClient
                .Child("admins")
                .PostAsync(admin);
        }

        public async Task<List<Admin>> GetAdminsAsync()
        {
            var admins = await _firebaseClient
                .Child("admins")
                .OnceAsync<Admin>();

            return admins.Select(a => a.Object).ToList();
        }
    }
}
