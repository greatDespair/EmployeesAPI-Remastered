using System.Data;
using Dapper;
using EmployeesAPI.Models.Database;
using EmployeesAPI.Data.Context;
using EmployeesAPI.Domain.Abstractions;
using EmployeesAPI.Domain.Resources;

namespace EmployeesAPI.Domain.Repositories
{
    public class EmployeeRepository : IRepository<Employee>, IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int?> Add(Employee item)
        {
            var queryString = SqlQueries.EmployeeInsert;

            return await _context.QueryFirstAsync<int?>(
                queryString,
                new
                {
                    Name = item.Name,
                    Surname = item.Surname,
                    Phone = item.Phone,
                    CompanyId = item.CompanyID,
                    PassportId = item.PassportId,
                    DepartmentId = item.DepartmentId
                });
        }

        public async Task Delete(int id)
        {
            var queryString = SqlQueries.EmployeeDelete;

            await _context.ExecuteAsync(queryString, new { Id = id });
        }

        public async Task<IEnumerable<EmployeeShortInfo>> GetAllByCompanyIdAsync(int id)
        {
            var queryString = SqlQueries.EmployeeGetAllByCompanyId;

            return await _context.QueryAsync<EmployeeShortInfo>(queryString, new { Id = id });
        }

        public async Task Update(Employee item)
        {
            var queryUpdEmployee = SqlQueries.EmployeeUpdate;

            await _context.ExecuteAsync(queryUpdEmployee, new
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
                Phone = item.Phone,
            });
        }

        public async Task<Employee?> ReadAsync(int employeeId)
        {
            var queryString = SqlQueries.EmployeeRead;

            return await _context.QueryFirstOrDefaultAsync<Employee>(queryString, new { Id = employeeId });
        }

        public async Task<int?> GetDepartmentIdAsync(int employeeId)
        {
            var queryString = SqlQueries.EmployeeGetDepartmentId;
            return await _context.QueryFirstOrDefaultAsync<int?>(queryString, new { Id = employeeId }); ;
        }

        public async Task<int?> GetPassportIdAsync(int employeeId)
        {
            var queryString = SqlQueries.EmployeeGetPassportId;
            return await _context.QueryFirstOrDefaultAsync<int?>(queryString, new { Id = employeeId }); ;
        }

        public async Task<IEnumerable<EmployeeShortInfo>> GetAllByDepNameAsync(string depName)
        {
            var queryString = SqlQueries.EmployeeGetAllByDepartmentName;
            return await _context.QueryAsync<EmployeeShortInfo>(queryString, new { depname = depName });
        }

    }
}
