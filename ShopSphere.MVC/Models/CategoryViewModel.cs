using System.ComponentModel.DataAnnotations;

namespace ShopSphere.MVC.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم التصنيف مطلوب")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "الاسم لازم يكون بين 2 و 50 حرف")]
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }
        
        public int? ParentCategoryId { get; set; }

        public string GetDisplayImageUrl()
        {
            if (!string.IsNullOrEmpty(ImageUrl)) return ImageUrl;
            
            var n = Name.ToLower();
            if (n.Contains("laptop") || n.Contains("computer") || n.Contains("pc") || n.Contains("macbook") || n.Contains("لابتوب") || n.Contains("كمبيوتر") || n.Contains("حاسوب")) return "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=800";
            if (n.Contains("phone") || n.Contains("mobile") || n.Contains("smartphone") || n.Contains("هاتف") || n.Contains("موبايل") || n.Contains("جوال")) return "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800";
            if (n.Contains("watch") || n.Contains("smartwatch") || n.Contains("ساعة")) return "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=800";
            if (n.Contains("camera") || n.Contains("photo") || n.Contains("lens") || n.Contains("كاميرا") || n.Contains("تصوير")) return "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=800";
            if (n.Contains("audio") || n.Contains("headphone") || n.Contains("earbud") || n.Contains("sound") || n.Contains("سماعة") || n.Contains("صوت")) return "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=800";
            if (n.Contains("vr") || n.Contains("virtual") || n.Contains("reality")) return "https://images.unsplash.com/photo-1622979135225-d2ba269cf1ac?w=800";
            if (n.Contains("game") || n.Contains("console") || n.Contains("play") || n.Contains("ألعاب") || n.Contains("العاب") || n.Contains("لعبة") || n.Contains("ألعاب وقيمينج") || n.Contains("قيمينج")) return "https://images.unsplash.com/photo-1486401899868-0e435ed85128?w=800";
            if (n.Contains("tv") || n.Contains("television") || n.Contains("screen") || n.Contains("monitor") || n.Contains("تلفزيون") || n.Contains("شاشة")) return "https://images.unsplash.com/photo-1593359677879-a4bb92f829d1?w=800";
            if (n.Contains("tablet") || n.Contains("ipad") || n.Contains("تابلت") || n.Contains("أيباد")) return "https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?w=800";
            if (n.Contains("ملابس") || n.Contains("أزياء") || n.Contains("موضة") || n.Contains("clothing") || n.Contains("fashion")) return "https://images.unsplash.com/photo-1489987707025-afc232f7ea0f?w=800";
            if (n.Contains("كتب") || n.Contains("كتاب") || n.Contains("رواية") || n.Contains("books") || n.Contains("book")) return "https://images.unsplash.com/photo-1497633762265-9d179a990aa6?w=800";
            if (n.Contains("منزل") || n.Contains("مطبخ") || n.Contains("أجهزة منزلية") || n.Contains("home") || n.Contains("kitchen")) return "https://images.unsplash.com/photo-1584622650111-993a426fbf0a?w=800";

            var fallbacks = new[] {
                "https://images.unsplash.com/photo-1518770660439-4636190af475?w=800",
                "https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=800",
                "https://images.unsplash.com/photo-1531297172867-4f541313ce8e?w=800"
            };
            return fallbacks[Id % fallbacks.Length];
        }
    }
}
