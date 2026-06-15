using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.MVC.Models;
using ShopSphere.MVC.Services;

namespace ShopSphere.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApiService _apiService;

        public UserController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var email = User.Identity?.Name ?? "user@shopsphere.com";
            
            // بنجيب الأوردرات من الـ API علطول ونفلترهم
            var allOrders = await _apiService.GetOrdersAsync();
            var userOrders = allOrders
                .Where(o => o.UserId == email || o.CustomerEmail == email)
                .ToList();

            var model = new UserDashboardViewModel
            {
                Email = email,
                FullName = email.Split('@')[0],
                RecentOrders = userOrders
            };
            
            return View(model);
        }
    }
}
