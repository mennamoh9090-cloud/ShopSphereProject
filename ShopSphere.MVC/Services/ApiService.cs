using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ShopSphere.MVC.Models;

namespace ShopSphere.MVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5062/api/"; 

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // بنحط البيز أدرس علطول
            _httpClient.BaseAddress = new Uri(_apiBaseUrl);
        }

        //(Products) 

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync(int? categoryId = null, string? search = null)
        {
            try
            {
                var url = "products";
                if (categoryId.HasValue || !string.IsNullOrEmpty(search))
                {
                    url += "?";
                    if (categoryId.HasValue) url += $"categoryId={categoryId}&";
                    if (!string.IsNullOrEmpty(search)) url += $"search={search}";
                }
                
                return await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>(url) ?? new List<ProductViewModel>();
            }
            catch
            {
                // بنرجع لستة فاضية لو حصل ايرور
                return new List<ProductViewModel>();
            }
        }

        public async Task<ProductViewModel?> GetProductDetailsAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ProductViewModel>($"products/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddProductAsync(ProductViewModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("products", model);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(int id, ProductViewModel model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"products/{id}", model);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"products/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        //  (Categories) 

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<CategoryViewModel>>("categories") ?? new List<CategoryViewModel>();
            }
            catch
            {
                return new List<CategoryViewModel>();
            }
        }

        public async Task<CategoryViewModel?> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CategoryViewModel>($"categories/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddCategoryAsync(CategoryViewModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("categories", model);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryViewModel model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"categories/{id}", model);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"categories/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        //  (Orders & Checkout)

        public async Task<OrderViewModel?> CheckoutAsync(OrderViewModel order)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("orders/checkout", order);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<OrderViewModel>();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<OrderViewModel>>("orders") ?? new List<OrderViewModel>();
            }
            catch
            {
                return new List<OrderViewModel>();
            }
        }

        public async Task<OrderViewModel?> GetOrderDetailsAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<OrderViewModel>($"orders/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string status)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"orders/{id}/status", status);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        //(Dashboard)

        public async Task<AdminDashboardViewModel?> GetAdminStatsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<AdminDashboardViewModel>("dashboard/admin");
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserDashboardViewModel>> GetUsersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<UserDashboardViewModel>>("dashboard/users") ?? new List<UserDashboardViewModel>();
            }
            catch
            {
                return new List<UserDashboardViewModel>();
            }
        }
    }
}
