using CompanyManagementMVC.DAL.Specification;

namespace CompanyManagementMVC.DAL.Repo.Abstraction
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetBySpecAsync(ISpecification<T> spec);           // ✅ جديد
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec);  // ✅ تعديل
        Task<int> CountAsync(ISpecification<T> spec);              // ✅ جديد
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}