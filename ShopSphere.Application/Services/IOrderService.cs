using System.Collections.Generic;
using System.Threading.Tasks;
using ShopSphere.Application.DTOs;

namespace ShopSphere.Application.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task UpdateStatusAsync(int id, string status);
    }
}
