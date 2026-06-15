using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ShopSphere.Application.DTOs;
using ShopSphere.Domain.Entities;
using ShopSphere.Domain.Repositories;

namespace ShopSphere.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var c = await _categoryRepository.GetByIdAsync(id);
            return c == null ? null : _mapper.Map<CategoryDto>(c);
        }

        public async Task AddAsync(CategoryDto dto)
        {
            var c = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(c);
        }

        public async Task UpdateAsync(CategoryDto dto)
        {
            var c = _mapper.Map<Category>(dto);
            _categoryRepository.Update(c);
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _categoryRepository.GetByIdAsync(id);
            if (c != null)
            {
                _categoryRepository.Delete(c);
            }
        }
    }
}
