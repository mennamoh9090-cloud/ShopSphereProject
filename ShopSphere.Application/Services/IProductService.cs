using System.Collections.Generic;
using System.Threading.Tasks;
using ShopSphere.Application.DTOs;

namespace ShopSphere.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string name);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task AddAsync(ProductDto dto);
        Task UpdateAsync(ProductDto dto);
        Task DeleteAsync(int id);
    }
}
