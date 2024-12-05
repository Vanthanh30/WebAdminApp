using Firebase.Database;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace WebAdminApp.Service
{
    public class FirebaseService
    {
        protected readonly FirebaseClient _firebaseClient;

        public FirebaseService(IConfiguration configuration)
        {
            // Read Firebase settings from configuration
            var firebaseConfig = configuration.GetSection("Firebase");
            var authSecret = firebaseConfig["AuthSecret"];
            var basePath = firebaseConfig["BasePath"];

            // Initialize FirebaseClient with Auth token
            _firebaseClient = new FirebaseClient(basePath, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(authSecret)
            });
        }
    }
}
