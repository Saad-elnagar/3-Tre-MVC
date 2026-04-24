using System.Linq.Expressions;

namespace CompanyManagementMVC.DAL.Repo.Abstraction
{
    public interface IGenericRepo<T> where T : ModelBase
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
        Task<int> SaveChangesAsync();
    }
}