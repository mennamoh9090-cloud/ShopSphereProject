namespace ShopSphere.Application.DTOs
{
    public class CheckoutDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = "CreditCard"; // CreditCard or CashOnDelivery
    }
}
