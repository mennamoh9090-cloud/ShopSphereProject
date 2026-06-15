using System;
using System.Collections.Generic;

namespace ShopSphere.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        
        // لو اليوزر مش مسجل دخول، هنحطها guest أو نسيبها فاضية
        public string UserId { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // تفاصيل الشحن والعميل اللي طلبناها في الشيك أوت
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;

        // حالة الطلب
        public string OrderStatus { get; set; } = "Pending"; // Pending, Processing, Shipped, Delivered

        // الدفع
        public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid, Failed
        public string StripeSessionId { get; set; } = string.Empty;
        public string StripePaymentIntentId { get; set; } = string.Empty;

        public ICollection<OrderItem>? Items { get; set; }
    }
}
