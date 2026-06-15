using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Domain.Entities;
using ShopSphere.Domain.Repositories;
using ShopSphere.Infrastructure.DbContext;

namespace ShopSphere.Infrastructure.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        // استخدام _context هنا لمخالفة الاتساق مع المجلدات التانية
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersWithDetailsAsync()
        {
            // بنجيب الأوردرات ومعاها تفاصيل المنتج
            return await _context.Orders
                .Include(o => o.Items!)
                .ThenInclude(i => i.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithDetailsByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Items!)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
