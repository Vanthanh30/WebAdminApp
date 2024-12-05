using Firebase.Database.Query;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using WebAdminApp.Models;

namespace WebAdminApp.Service
{
    public class AuthenticationService : FirebaseService
    {
        public AuthenticationService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> ValidateUserAsync(LoginModel login)
        {
            // Kiểm tra thông tin người dùng trong Firebase
            var admins = await _firebaseClient
                .Child("admins")
                .OnceAsync<Admin>();

            // Tìm người dùng có email khớp
            var user = admins
                .Select(a => a.Object)
                .FirstOrDefault(a => a.Email == login.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                // Mật khẩu đúng
                return true;
            }

            // Không tìm thấy người dùng hoặc mật khẩu sai
            return false;
        }
    }

}
