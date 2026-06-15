namespace ShopSphere.MVC.Models;

using System.Collections.Generic;

public class HomeViewModel
{
    public IEnumerable<ProductViewModel> FeaturedProducts { get; set; } = [];
    public IEnumerable<CategoryViewModel> Categories { get; set; } = [];
}
