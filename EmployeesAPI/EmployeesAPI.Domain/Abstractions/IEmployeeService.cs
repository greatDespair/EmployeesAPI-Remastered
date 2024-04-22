using EmployeesAPI.Models.Database;
using EmployeesAPI.Models.Dto;

namespace EmployeesAPI.Domain.Abstractions
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeDto>> GetEmployeesByDepartmentName(string name);

        public Task<List<EmployeeDto>> GetEmployeesByCompanyId(int id);

        public Task<int?> AddEmployee(EmployeeDto employee);

        public Task UpdateEmployee(EmployeeDto employee);

        public Task DeleteEmployee(int id);
    }
}
