using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopSphere.Domain.Repositories
{
    // واجهة عامة للـ Repository عشان نسهل الشغل
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
