using EmployeesAPI.Models.Database;

namespace EmployeesAPI.Domain.Abstractions
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<EmployeeShortInfo>> GetAllByDepartmentName(string depName);

        Task<IEnumerable<EmployeeShortInfo>> GetAllByCompanyId(int compId);

        Task<int?> GetPassportId(int employeeId);

        Task<int?> GetDepartmentId(int employeeId);

        Task<Employee?> ReadAsync(int employeeId);
    }
}
