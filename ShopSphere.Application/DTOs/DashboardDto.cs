namespace ShopSphere.Application.DTOs;


public class AdminDashboardDto
{
    public decimal TotalSales { get; set; }
    public int OrdersCount { get; set; }
    public int UsersCount { get; set; }
    public double RevenueGrowthPercentage { get; set; }
}

public class UserDashboardDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int ActiveOrdersCount { get; set; }
    public int WishlistItemsCount { get; set; }
    public System.Collections.Generic.IEnumerable<OrderDto> RecentOrders { get; set; } = [];
}
