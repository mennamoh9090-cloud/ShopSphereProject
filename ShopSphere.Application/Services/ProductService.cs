using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ShopSphere.Application.DTOs;
using ShopSphere.Domain.Entities;
using ShopSphere.Domain.Repositories;

namespace ShopSphere.Application.Services
{
    public class ProductService : IProductService
    {
        // استخدام _repo هنا
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            // بنجيب المنتج بالـ ID بتاعه
            var p = await _repo.GetByIdAsync(id);
            return p == null ? null : _mapper.Map<ProductDto>(p);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            // بنجيب كل المنتجات علطول
            var products = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string name)
        {
            var products = await _repo.SearchProductsAsync(name);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _repo.GetProductsByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task AddAsync(ProductDto dto)
        {
            var p = _mapper.Map<Product>(dto);
            await _repo.AddAsync(p);
        }

        public async Task UpdateAsync(ProductDto dto)
        {
            var p = _mapper.Map<Product>(dto);
            _repo.Update(p);
        }

        public async Task DeleteAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p != null)
            {
                _repo.Delete(p);
            }
        }
    }
}