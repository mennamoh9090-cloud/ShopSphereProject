using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ShopSphere.MVC.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
