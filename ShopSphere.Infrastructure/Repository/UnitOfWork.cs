using System.Threading.Tasks;
using ShopSphere.Domain.Repositories;
using ShopSphere.Infrastructure.DbContext;

namespace ShopSphere.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        // استخدام db هنا برضه نوع من التغيير في التسمية
        private readonly ApplicationDbContext db;
        
        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
            Products = new ProductRepository(db);
            Categories = new CategoryRepository(db);
            Orders = new OrderRepository(db);
        }

        public async Task<int> CompleteAsync()
        {
            // بنحفظ كل التغييرات اللي حصلت في ترانزكشن واحد
            return await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
