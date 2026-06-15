using System;
using System.Threading.Tasks;

namespace ShopSphere.Domain.Repositories
{
    // الـ Unit of Work عشان ننسق العمليات كلها في ترانزكشن واحد
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        Task<int> CompleteAsync();
    }
}
