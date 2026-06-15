using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Application.DTOs;
using ShopSphere.MVC.Models;
using ShopSphere.MVC.Services;

namespace ShopSphere.MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApiService _apiService;

        public CheckoutController(ApiService apiService)
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
            return JsonSerializer.Deserialize<List<CartItemViewModel>>(cartJson) ?? new List<CartItemViewModel>();
        }

        public IActionResult Index()
        {
            var cartItems = GetCartFromSession();
            var cartVm = new CartViewModel
            {
                Items = cartItems
            };

            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "السلة فارغة، يرجى إضافة منتجات أولاً.";
                return RedirectToAction("Index", "Home");
            }

            return View(cartVm);
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessCheckout([FromBody] CheckoutDto dto)
        {
            if (dto == null)
            {
                return BadRequest("بيانات الطلب غير صالحة");
            }

            var cartItems = GetCartFromSession();
            if (!cartItems.Any())
            {
                return BadRequest("السلة فارغة");
            }

            decimal subTotal = cartItems.Sum(i => i.Total);
            decimal tax = subTotal * 0.04m;
            decimal grandTotal = subTotal + tax;

            // بنجهز الـ OrderViewModel اللي هييتبعت للـ API
            var isCod = dto.PaymentMethod == "CashOnDelivery";
            var orderVm = new OrderViewModel
            {
                UserId = User.Identity?.IsAuthenticated == true ? (User.Identity.Name ?? "guest") : "guest",
                CustomerName = dto.CustomerName,
                CustomerEmail = dto.CustomerEmail,
                ShippingAddress = dto.ShippingAddress,
                CustomerPhone = dto.CustomerPhone,
                TotalPrice = grandTotal,
                CreatedAt = DateTime.UtcNow,
                OrderStatus = "Pending",
                PaymentStatus = isCod ? "Pending" : "Paid",
                StripeSessionId = isCod ? "COD" : "mock_" + Guid.NewGuid().ToString("N"),
                Items = cartItems.Select(ci => new OrderItemViewModel
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.ProductName,
                    Price = ci.Price,
                    Quantity = ci.Quantity
                }).ToList()
            };

            // بنرسل الطلب للـ API 
            var result = await _apiService.CheckoutAsync(orderVm);
            if (result != null)
            {
                // بنفضي السلة بعد الدفع بنجاح
                HttpContext.Session.Remove(GetCartSessionKey());
                return Ok(new { success = true, orderId = result.Id });
            }

            return BadRequest("فشلت عملية إنشاء الطلب من الـ API");
        }
    }
}
