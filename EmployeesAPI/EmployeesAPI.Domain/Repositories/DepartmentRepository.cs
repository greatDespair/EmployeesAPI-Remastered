using EmployeesAPI.Data.Context;
using EmployeesAPI.Domain.Abstractions;
using EmployeesAPI.Domain.Resources;
using EmployeesAPI.Models.Database;

namespace EmployeesAPI.Domain.Repositories
{
    public class DepartmentRepository : IRepository<Department>, IDepartmentRepository
    {
        private readonly DapperContext _context;

        public DepartmentRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<int?> Add(Department item)
        {
            var queryString = SqlQueries.DepartmentInsert;

            return await _context.QueryFirstOrDefaultAsync<int?>(queryString, new { DepartmentName = item.DepartmentName, DepartmentPhone = item.DepartmentPhone });
        }

        public async Task Delete(int id)
        {
            var queryString = SqlQueries.DepartmentDelete;

            await _context.ExecuteAsync(queryString, new { Id = id });
        }

        public async Task<Department?> ReadByNameAsync(string name)
        {
            var queryString = SqlQueries.DepartmentReadByName;

            return await _context.QueryFirstOrDefaultAsync<Department>(queryString, new { DepartmentName = name });
        }

        public async Task Update(Department item)
        {
            var queryString = SqlQueries.DepartmentUpdate;

            await _context.ExecuteAsync(queryString, new { Id = item.Id, DepartmentName = item.DepartmentName, DepartmentPhone = item.DepartmentPhone });
        }
    }
}
