using System.ComponentModel.DataAnnotations;

namespace ShopSphere.MVC.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم المنتج مطلوب جداً ومينفعش تسيبه فاضي")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "الاسم لازم يكون بين 3 و 100 حرف")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "السعر مطلوب")]
        [Range(0.1, 1000000, ErrorMessage = "السعر لازم يكون أكبر من 0")]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "صورة المنتج مطلوبة")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "التصنيف مطلوب")]
        public int CategoryId { get; set; }

        public CategoryViewModel? Category { get; set; }

        [Required(ErrorMessage = "الكمية في المخزن مطلوبة")]
        [Range(0, 10000, ErrorMessage = "المخزن لازم يكون بين 0 و 10000")]
        public int Stock { get; set; }
    }
}
