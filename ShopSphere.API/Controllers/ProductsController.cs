using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Application.DTOs;
using ShopSphere.Application.Services;

namespace ShopSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // بنستخدم هنا الـ IProductService
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? categoryId, [FromQuery] string? search)
        {
            //  لو باعت سيرش بنبحث بالاسم، لو باعت تصنيف بنجيب بالتصنيف، غير كدة بنجيب كلو
            if (!string.IsNullOrEmpty(search))
            {
                var searchResult = await _service.SearchProductsAsync(search);
                return Ok(searchResult);
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                var categoryResult = await _service.GetProductsByCategoryAsync(categoryId.Value);
                return Ok(categoryResult);
            }

            var all = await _service.GetAllAsync();
            return Ok(all);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _service.GetByIdAsync(id);
            if (p == null) 
            {
                return NotFound();
            }
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDto dto)
        {
            // حفظ المنتج الجديد
            await _service.AddAsync(dto);
            return Ok(new { success = true });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            if (id != dto.Id) 
            {
                return BadRequest("المعرف غير متطابق");
            }
            await _service.UpdateAsync(dto);
            return Ok(new { success = true });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { success = true });
        }
    }
}