using System.Collections.Generic;
using System.Threading.Tasks;
using ShopSphere.Domain.Entities;

namespace ShopSphere.Domain.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersWithDetailsAsync();
        Task<Order?> GetOrderWithDetailsByIdAsync(int id);
    }
}
