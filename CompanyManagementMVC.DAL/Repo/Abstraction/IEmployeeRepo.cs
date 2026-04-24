namespace CompanyManagementMVC.DAL.Repo.Abstraction
{
    public interface IEmployeeRepo : IGenericRepo<Employee>
    {
      
        Task<bool> ToggleStateAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateEmployeeAsync(int id, string name, decimal salary);
    }
}