using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.MVC.Services;
using ShopSphere.MVC.Models;

namespace ShopSphere.MVC.Controllers
{
    public class CartController : Controller
    {
        // بنستخدم الـ API سيرفيس هنا عشان نجيب تفاصيل المنتج
        private readonly ApiService _apiService;

        public CartController(ApiService apiService)
        {
            _apiService = apiService;
        }

        private string GetCartSessionKey()
        {
            var userName = User.Identity?.IsAuthenticated == true ? User.Identity.Name : "Anonymous";
            return $"CartSession_{userName}";
        }

        private List<CartItemViewModel> GetCartFromSession()
        {
            var key = GetCartSessionKey();
            var cartJson = HttpContext.Session.GetString(key);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItemViewModel>();
            }
            try
            {
                return JsonSerializer.Deserialize<List<CartItemViewModel>>(cartJson) ?? new List<CartItemViewModel>();
            }
            catch
            {
                // لو حصل أي ايرور بنرجع لستة فاضية وخلاص
                return new List<CartItemViewModel>();
            }
        }

        private void SaveCartToSession(List<CartItemViewModel> cart)
        {
            var key = GetCartSessionKey();
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(key, cartJson);
        }

        public IActionResult Index()
        {
            var items = GetCartFromSession();
            var cartVm = new CartViewModel
            {
                Items = items
            };
            
            // بنحط رسالة في الفيو باج للتجربة
            ViewBag.Message = "سلة التسوق الخاصة بك";
            return View(cartVm);
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = GetCartFromSession();
            int count = cart.Sum(i => i.Quantity);
            return Json(new { count });
        }

        public class AddToCartRequest 
        { 
            public int ProductId { get; set; } 
            public int Quantity { get; set; } 
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddToCartRequest req)
        {
            // بنجيب المنتج من الـ API
            var product = await _apiService.GetProductDetailsAsync(req.ProductId);
            if (product == null)
            {
                return NotFound(new { success = false, message = "المنتج مش موجود" });
            }

            var cart = GetCartFromSession();
            var existingItem = cart.FirstOrDefault(i => i.ProductId == req.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += req.Quantity;
            }
            else
            {
                cart.Add(new CartItemViewModel
                {
                    Id = cart.Count + 1, // id بسيط كدة وخلاص
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = req.Quantity
                });
            }

            SaveCartToSession(cart);
            return Ok(new { success = true });
        }

        public class RemoveFromCartRequest 
        { 
            public int CartItemId { get; set; } 
        }

        [HttpPost]
        public IActionResult Remove([FromBody] RemoveFromCartRequest req)
        {
            var cart = GetCartFromSession();
            // بنحاول نمسح بالـ ProductId أو الـ Id بتاع العنصر في السيشن
            var item = cart.FirstOrDefault(i => i.Id == req.CartItemId || i.ProductId == req.CartItemId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCartToSession(cart);
                return Ok(new { success = true });
            }
            return BadRequest(new { success = false, message = "العنصر مش موجود في السلة" });
        }
    }
}
