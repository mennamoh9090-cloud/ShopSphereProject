using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Application.DTOs;
using ShopSphere.Application.Services;

namespace ShopSphere.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] OrderDto dto)
        {
            if (dto == null) return BadRequest("بيانات الطلب فارغة");
            
            // بنسيف الأوردر في قاعدة البيانات
            var created = await _orderService.CreateOrderAsync(dto);
            return Ok(created);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            await _orderService.UpdateStatusAsync(id, status);
            return Ok(new { success = true });
        }
    }
}
