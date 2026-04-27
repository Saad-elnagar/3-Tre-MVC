
using CompanyManagementMVC.DAL.Repo.Abstraction;
using CompanyManagementMVC.DAL.Specification;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementMVC.DAL.Repo.Implementation
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
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

        // ✅ جديد: GetBySpec
        public virtual async Task<T?> GetBySpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).FirstOrDefaultAsync();

        // ✅ تعديل: GetAll بالـ Spec
        public virtual async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).ToListAsync();

        // ✅ جديد: Count
        public virtual async Task<int> CountAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).CountAsync();

        public virtual async Task AddAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public virtual void Update(T entity)
            => _dbSet.Update(entity);

        public virtual void Delete(T entity)
            => _dbSet.Remove(entity);

        public virtual async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        // ✅ الـ Core بتاع الـ Pattern
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
        }
    }
}