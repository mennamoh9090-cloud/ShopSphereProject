using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Domain.Entities;

namespace ShopSphere.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Customer> Customers { get; set; } // الكيان الجديد للعملاء

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>().Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Entity<OrderItem>().Property(o => o.Price).HasColumnType("decimal(18,2)");
            builder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "إلكترونيات", Description = "موبايلات، لابتوبات، وأجهزة ذكية", ImageUrl = "https://images.unsplash.com/photo-1498049794561-7780e7231661?w=800" },
                new Category { Id = 2, Name = "ملابس", Description = "ملابس كاجوال ورسمية لجميع الأعمار", ImageUrl = "https://images.unsplash.com/photo-1489987707025-afc232f7ea0f?w=800" },
                new Category { Id = 3, Name = "كتب", Description = "روايات، كتب علمية، وتطوير الذات", ImageUrl = "https://images.unsplash.com/photo-1497633762265-9d179a990aa6?w=800" },
                new Category { Id = 4, Name = "أجهزة منزلية", Description = "خلاطات، ماكينات قهوة، وأدوات مطبخ", ImageUrl = "https://images.unsplash.com/photo-1584622650111-993a426fbf0a?w=800" },
                new Category { Id = 5, Name = "ألعاب وقيمينج", Description = "أجهزة بلايستيشن، كروت شاشة، واكسسوارات ألعاب", ImageUrl = "https://images.unsplash.com/photo-1607604276583-eef5d076aa5f?w=800" }
            );

            // seeding لبعض المنتجات عشان الموقع يشتغل علطول
            builder.Entity<Product>().HasData(
                new Product 
                { 
                    Id = 1, 
                    Name = "سماعة بلوتوث لاسلكية", 
                    Price = 1500.00m, 
                    Description = "سماعة عازلة للصوت ومقاومة للماء مع بطارية تدوم 24 ساعة", 
                    ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=800", 
                    Stock = 10, 
                    CategoryId = 1 
                },
                new Product 
                { 
                    Id = 2, 
                    Name = "لابتوب ألعاب برو", 
                    Price = 45000.00m, 
                    Description = "لابتوب بمواصفات عالية للألعاب والبرمجة والمونتاج مع كارت شاشة RTX 4060", 
                    ImageUrl = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=800", 
                    Stock = 5, 
                    CategoryId = 1 
                },
                new Product 
                { 
                    Id = 3, 
                    Name = "قميص كاجوال كلاسيك", 
                    Price = 750.00m, 
                    Description = "قميص قطن 100% مريح جداً ومناسب لجميع الخروجات", 
                    ImageUrl = "https://images.unsplash.com/photo-1581655353564-df123a1eb820?w=800", 
                    Stock = 20, 
                    CategoryId = 2 
                },
                new Product 
                { 
                    Id = 4, 
                    Name = "رواية أرض زيكولا", 
                    Price = 120.00m, 
                    Description = "كتاب ورواية أرض زيكولا المشهورة للكاتب عمرو عبد الحميد", 
                    ImageUrl = "https://images.unsplash.com/photo-1544947950-fa07a98d237f?w=800", 
                    Stock = 15, 
                    CategoryId = 3 
                },
                new Product 
                { 
                    Id = 5, 
                    Name = "هاتف ذكي ألترا برو", 
                    Price = 28000.00m, 
                    Description = "شاشة AMOLED 120Hz، كاميرا بدقة 200 ميجابكسل، وبطارية ضخمة", 
                    ImageUrl = "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800", 
                    Stock = 8, 
                    CategoryId = 1 
                },
                new Product 
                { 
                    Id = 6, 
                    Name = "ساعة ذكية رياضية", 
                    Price = 3200.00m, 
                    Description = "مراقبة ضربات القلب والأنشطة اليومية، مقاومة للماء بالكامل", 
                    ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=800", 
                    Stock = 12, 
                    CategoryId = 1 
                },
                new Product 
                { 
                    Id = 7, 
                    Name = "حذاء رياضي خفيف", 
                    Price = 1800.00m, 
                    Description = "نعل مرن ومريح للمشي والجري لمسافات طويلة", 
                    ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=800", 
                    Stock = 25, 
                    CategoryId = 2 
                },
                new Product 
                { 
                    Id = 8, 
                    Name = "جاكيت جلد أسود عصري", 
                    Price = 3500.00m, 
                    Description = "جلد طبيعي ممتاز، بتصميم كلاسيكي رائع للطقس البارد", 
                    ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=800", 
                    Stock = 10, 
                    CategoryId = 2 
                },
                new Product 
                { 
                    Id = 9, 
                    Name = "كتاب الأب الغني والأب الفقير", 
                    Price = 250.00m, 
                    Description = "أهم كتاب في التثقيف المالي وإدارة الأموال وتطوير الذات", 
                    ImageUrl = "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?w=800", 
                    Stock = 30, 
                    CategoryId = 3 
                },
                new Product 
                { 
                    Id = 10, 
                    Name = "رواية الفيل الأزرق", 
                    Price = 150.00m, 
                    Description = "رواية الإثارة والغموض الأكثر مبيعاً للكاتب أحمد مراد", 
                    ImageUrl = "https://images.unsplash.com/photo-1543002588-bfa74002ed7e?w=800", 
                    Stock = 18, 
                    CategoryId = 3 
                },
                new Product 
                { 
                    Id = 11, 
                    Name = "خلاط كهربائي سريع", 
                    Price = 2400.00m, 
                    Description = "قوة 1200 واط مع إناء زجاجي متين لتحضير العصائر والشوربات", 
                    ImageUrl = "https://images.unsplash.com/photo-1578643463396-0997cb5328c1?w=800", 
                    Stock = 7, 
                    CategoryId = 4 
                },
                new Product 
                { 
                    Id = 12, 
                    Name = "ذراع تحكم ألعاب لاسلكي", 
                    Price = 1600.00m, 
                    Description = "متوافق مع الكمبيوتر والبلايستيشن، استجابة سريعة جداً وبدون سلك", 
                    ImageUrl = "https://images.unsplash.com/photo-1531525645387-7f14be1bdbbd?w=800", 
                    Stock = 14, 
                    CategoryId = 5 
                }
            );
        }
    }
}