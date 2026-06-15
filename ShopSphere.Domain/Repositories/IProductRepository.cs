using System.Collections.Generic;
using System.Threading.Tasks;
using ShopSphere.Domain.Entities;

namespace ShopSphere.Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // ميثود زيادة عشان البحث بالاسم
        Task<IEnumerable<Product>> SearchProductsAsync(string searchName);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    }
}
