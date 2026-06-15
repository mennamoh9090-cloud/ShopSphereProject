using ShopSphere.Domain.Entities;
using ShopSphere.Domain.Repositories;
using ShopSphere.Infrastructure.DbContext;

namespace ShopSphere.Infrastructure.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
