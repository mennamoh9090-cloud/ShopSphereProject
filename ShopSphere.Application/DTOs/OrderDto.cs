using System;
using System.Collections.Generic;

namespace ShopSphere.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;

        public string OrderStatus { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string StripeSessionId { get; set; } = string.Empty;
        public string StripePaymentIntentId { get; set; } = string.Empty;

        public List<OrderItemDto> Items { get; set; } = [];
    }
}
