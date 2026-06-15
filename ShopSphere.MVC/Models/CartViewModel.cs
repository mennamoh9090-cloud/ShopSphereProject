using System.Collections.Generic;
using System.Linq;

namespace ShopSphere.MVC.Models;

public class CartItemViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Total => Price * Quantity;
}

public class CartViewModel
{
    public List<CartItemViewModel> Items { get; set; } = new();
    public decimal SubTotal => Items.Sum(i => i.Total);
    public decimal Tax => SubTotal * 0.04m; 
    public decimal Total => SubTotal + Tax;
}
