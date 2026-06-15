using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.MVC.Services;
using ShopSphere.MVC.Models;

namespace ShopSphere.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApiService _apiService;

        public CategoryController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _apiService.GetCategoriesAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _apiService.GetCategoryByIdAsync(id);
            if (category == null) 
            {
                return NotFound();
            }

            var categories = await _apiService.GetCategoriesAsync();
            var subcategories = categories.Where(c => c.ParentCategoryId == id).ToList();

            ViewBag.ParentCategory = category;
            return View(subcategories);
        }
    }
}
