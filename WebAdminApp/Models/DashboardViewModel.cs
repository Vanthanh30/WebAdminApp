using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdminApp.Models
{
    public class DashboardViewModel
    {
        public int TotalRecipes { get; set; }
        public int ActiveRecipes { get; set; }
        public int InactiveRecipes { get; set; }
        public int TotalAccounts { get; set; }
    }
}
