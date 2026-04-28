using CompanyManagementMVC.DAL.Specification;

namespace CompanyManagementMVC.DAL.Repo.Abstraction
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetBySpecAsync(ISpecification<T> spec);          
        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec); 
       // Task<int> CountAsync(ISpecification<T> spec);             
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}