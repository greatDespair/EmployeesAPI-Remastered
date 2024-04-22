using EmployeesAPI.Data.Context;
using EmployeesAPI.Domain.Abstractions;
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
            var queryString = "INSERT INTO public.\"Departments\" (DepartmentName, DepartmentPhone) VALUES (@DepartmentName, @DepartmentPhone) RETURNING Id;";

            return await _context.QueryFirstOrDefaultAsync<int?>(queryString, new { DepartmentName = item.DepartmentName, DepartmentPhone = item.DepartmentPhone });
        }

        public async Task Delete(int id)
        {
            var queryString = "DELETE FROM public.\"Departments\" WHERE Id = @Id;";

            await _context.ExecuteAsync(queryString, new { Id = id });
        }

        public async Task<Department?> ReadByNameAsync(string name)
        {
            var queryString = "SELECT * FROM public.\"Departments\" WHERE DepartmentName = @DepartmentName";
            return await _context.QueryFirstOrDefaultAsync<Department>(queryString, new { DepartmentName = name });
        }

        public async Task Update(Department item)
        {
            var queryString = "UPDATE public.\"Departments\" SET " +
                    "DepartmentName = CASE WHEN @DepartmentName IS NULL THEN DepartmentName ELSE @DepartmentName END, " +
                    "DepartmentPhone = CASE WHEN @DepartmentPhone IS NULL THEN DepartmentPhone ELSE @DepartmentPhone END " +
                    "WHERE Id = @Id;";
            await _context.ExecuteAsync(queryString, new { Id = item.Id, DepartmentName = item.DepartmentName, DepartmentPhone = item.DepartmentPhone });
        }
    }
}
