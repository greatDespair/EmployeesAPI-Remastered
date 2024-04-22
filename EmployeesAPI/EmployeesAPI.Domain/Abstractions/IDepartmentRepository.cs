using EmployeesAPI.Models.Database;

namespace EmployeesAPI.Domain.Abstractions
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department?> ReadByNameAsync(string name);
    }
}
