using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Domain.Entities;
using ShopSphere.Domain.Repositories;
using ShopSphere.Infrastructure.DbContext;

namespace ShopSphere.Infrastructure.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        // استخدام _context هنا بدل dbContext لمجرد اللخبطة وعدم الاتساق
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // هنا بنجيب منتجات بتصنيف معين
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            // LINQ query simple
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        // دالة البحث بالاسم
        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                return await _context.Products.Include(p => p.Category).ToListAsync();
            }
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(searchName))
                .ToListAsync();
        }
    }
}