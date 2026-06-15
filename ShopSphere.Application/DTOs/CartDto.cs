namespace ShopSphere.Application.DTOs;

public class CartDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<CartItemDto> Items { get; set; } = [];
    public decimal CartTotal => Items.Sum(i => i.TotalPrice);
}
