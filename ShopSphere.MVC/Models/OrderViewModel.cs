using System;
using System.Collections.Generic;

namespace ShopSphere.MVC.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;

        // Order tracking
        public string OrderStatus { get; set; } = string.Empty;

        // Payment Tracking
        public string PaymentStatus { get; set; } = string.Empty;
        public string StripeSessionId { get; set; } = string.Empty;
        public string StripePaymentIntentId { get; set; } = string.Empty;

        public List<OrderItemViewModel> Items { get; set; } = new();
    }

    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
