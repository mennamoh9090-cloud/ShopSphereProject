using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.MVC.Models;
using ShopSphere.MVC.Services;

namespace ShopSphere.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApiService _apiService;

        public AdminController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var stats = await _apiService.GetAdminStatsAsync();
            var model = stats ?? new AdminDashboardViewModel();
            
            var testUnused = "admin_dashboard_render";

            return View(model);
        }

        public IActionResult ManageProducts()
        {
            var products = _apiService.GetProductsAsync().Result;
            return View(products);
        }

        // GET
        public async Task<IActionResult> AddProduct()
        {
            ViewBag.Categories = await _apiService.GetCategoriesAsync();
            return View(new ProductViewModel());
        }

        //-POST
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiService.AddProductAsync(model);
                if (success)
                {
                    TempData["SuccessMessage"] = "تم إضافة المنتج بنجاح عبر الـ API!";
                    return RedirectToAction("ManageProducts");
                }
                ViewBag.ErrorMessage = "فشل في حفظ المنتج بالـ API.";
            }

            ViewBag.Categories = await _apiService.GetCategoriesAsync();
            return View(model);
        }

        // تعديل منتج - GET
        public async Task<IActionResult> EditProduct(int id)
        {
            var p = await _apiService.GetProductDetailsAsync(id);
            if (p == null) return NotFound();

            ViewBag.Categories = await _apiService.GetCategoriesAsync();
            return View(p);
        }

        // تعديل منتج - POST
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, ProductViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var success = await _apiService.UpdateProductAsync(id, model);
                if (success)
                {
                    TempData["SuccessMessage"] = "تم تعديل المنتج بنجاح!";
                    return RedirectToAction("ManageProducts");
                }
            }

            ViewBag.Categories = await _apiService.GetCategoriesAsync();
            return View(model);
        }

        //حذف منتج ـ Result
        public IActionResult DeleteProduct(int id)
        {
            var success = _apiService.DeleteProductAsync(id).Result;
            if (success)
            {
                TempData["SuccessMessage"] = "تم حذف المنتج بنجاح.";
            }
            return RedirectToAction("ManageProducts");
        }

        // عرض التصنيفات
        public async Task<IActionResult> Categories()
        {
            var cats = await _apiService.GetCategoriesAsync();
            return View(cats);
        }

        // إضافة تصنيف - GET
        public IActionResult AddCategory()
        {
            return View(new CategoryViewModel());
        }

        // إضافة تصنيف - POST
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiService.AddCategoryAsync(model);
                if (success)
                {
                    TempData["SuccessMessage"] = "تم إضافة تصنيف جديد.";
                    return RedirectToAction("Categories");
                }
            }
            return View(model);
        }

        // تعديل تصنيف - GET
        public async Task<IActionResult> EditCategory(int id)
        {
            var cat = await _apiService.GetCategoryByIdAsync(id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        // تعديل تصنيف - POST
        [HttpPost]
        public async Task<IActionResult> EditCategory(int id, CategoryViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var success = await _apiService.UpdateCategoryAsync(id, model);
                if (success)
                {
                    TempData["SuccessMessage"] = "تم تعديل التصنيف بنجاح.";
                    return RedirectToAction("Categories");
                }
            }
            return View(model);
        }

        // حذف تصنيف
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _apiService.DeleteCategoryAsync(id);
            TempData["SuccessMessage"] = "تم حذف التصنيف.";
            return RedirectToAction("Categories");
        }

        // عرض المستخدمين
        public async Task<IActionResult> Users()
        {
            var users = await _apiService.GetUsersAsync();
            return View(users);
        }

        // عرض الطلبات
        public async Task<IActionResult> Orders()
        {
            var orders = await _apiService.GetOrdersAsync();
            return View(orders);
        }

        // تفاصيل الطلب - Partial
        public async Task<IActionResult> GetOrderDetailsPartial(int id)
        {
            var order = await _apiService.GetOrderDetailsAsync(id);
            if (order == null) return NotFound();
            return PartialView("_OrderDetailsPartial", order);
        }

        // تحديث حالة الطلب
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int id, string status)
        {
            await _apiService.UpdateOrderStatusAsync(id, status);
            TempData["SuccessMessage"] = "تم تحديث حالة الطلب.";
            return RedirectToAction("Orders");
        }
    }
}
