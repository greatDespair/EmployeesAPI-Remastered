using EmployeesAPI.Models.Database;
using EmployeesAPI.Models.Dto;

namespace EmployeesAPI.Domain.Abstractions
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeDto>> GetEmployeesByDepNameAsync(string name);

        public Task<List<EmployeeDto>> GetEmployeesByCompanyIdAsync(int id);

        public Task<int?> AddEmployeeAsync(AddedEmployeeDto employee);

        public Task UpdateEmployeeAsync(EmployeeDto employee);

        public Task DeleteEmployeeAsync(int id);
    }
}
