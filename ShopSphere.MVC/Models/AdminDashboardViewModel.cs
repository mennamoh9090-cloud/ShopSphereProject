using System.Collections.Generic;

namespace ShopSphere.MVC.Models
{
    public class AdminDashboardViewModel
    {
        public decimal TotalSales { get; set; }
        public int OrdersCount { get; set; }
        public int UsersCount { get; set; }
        public double RevenueGrowthPercentage { get; set; }
        public List<ProductViewModel> RecentProducts { get; set; } = new();

        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
    }
}
