using CompanyManagementMVC.DAL.DataBase;
using CompanyManagementMVC.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CompanyManagementMVC.DAL.Repo.Implementation
{
    public class GenericRepo<T> : IGenericRepo<T> where T : ModelBase
    {
        protected readonly AppDbcontext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepo(AppDbcontext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            if (filter != null)
                return await _dbSet.Where(filter).ToListAsync();
            return await _dbSet.ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public virtual void Update(T entity)
            => _dbSet.Update(entity);

        public virtual void Delete(T entity)
            => _dbSet.Remove(entity);

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
            => await _dbSet.AnyAsync(filter);

        public virtual async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}