using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Infrastructure.DbContext;

namespace ShopSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public DashboardController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminStats()
        {
            var totalProducts = await db.Products.CountAsync();
            var totalCategories = await db.Categories.CountAsync();
            var totalOrders = await db.Orders.CountAsync();
            var totalSales = await db.Orders
                .Where(o => o.OrderStatus != "Cancelled")
                .SumAsync(o => o.TotalPrice);

            //  بنجيب آخر 5 منتجات
            var recent = await db.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .Take(5)
                .ToListAsync();

            return Ok(new
            {
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                OrdersCount = totalOrders,
                UsersCount = db.Users.Count(), 
                RevenueGrowthPercentage = 12.5,
                TotalSales = totalSales,
                RecentProducts = recent.Select(p => new {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.ImageUrl,
                    p.CategoryId,
                    Category = p.Category != null ? new { p.Category.Id, p.Category.Name } : null,
                    p.Stock
                }).ToList()
            });
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            // بنجيب كل اليوزرز من قاعدة البيانات
            var users = db.Users.ToList().Select(u => new
            {
                Email = u.Email ?? "",
                FullName = u.UserName ?? ""
            }).ToList();

            return Ok(users);
        }
    }
}
