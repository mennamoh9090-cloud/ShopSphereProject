using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.MVC.Services;

namespace ShopSphere.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApiService _apiService;

        public ProductController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            // بنستدعي الـ API عشان نجيب المنتجات حسب التصنيف
            var products = await _apiService.GetProductsAsync(categoryId);
            if (categoryId.HasValue)
            {
                var category = await _apiService.GetCategoryByIdAsync(categoryId.Value);
                ViewBag.CategoryName = category?.Name;
            }
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _apiService.GetProductDetailsAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
