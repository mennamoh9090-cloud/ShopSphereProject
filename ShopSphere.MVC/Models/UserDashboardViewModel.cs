using System.Collections.Generic;

namespace ShopSphere.MVC.Models;

public class UserDashboardViewModel
{
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int ActiveOrdersCount { get; set; }
    public int WishlistItemsCount { get; set; }
    public IEnumerable<OrderViewModel>? RecentOrders { get; set; }
}
