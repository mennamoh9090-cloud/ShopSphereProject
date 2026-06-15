using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Domain.Repositories;
using ShopSphere.Infrastructure.DbContext;

namespace ShopSphere.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // استخدام dbContext هنا عشان يبان الاختلاف بينه وبين _context في الأماكن التانية
        protected readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            // جونيور ديف: سيف تشينجز هنا علطول من غير ما يستنى الـ Unit of Work في بعض الأحيان
            await dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            // بنعمل حفظ علطول برضه
            dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            dbContext.SaveChanges();
        }
    }
}
