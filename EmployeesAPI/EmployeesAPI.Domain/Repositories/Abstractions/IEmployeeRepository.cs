using EmployeesAPI.Models.Database;

namespace EmployeesAPI.Domain.Abstractions
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<EmployeeShortInfo>> GetAllByDepNameAsync(string depName);

        Task<IEnumerable<EmployeeShortInfo>> GetAllByCompanyIdAsync(int compId);

        Task<int?> GetPassportIdAsync(int employeeId);

        Task<int?> GetDepartmentIdAsync(int employeeId);

        Task<Employee?> ReadAsync(int employeeId);
    }
}
