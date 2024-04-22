using System.Data;
using Dapper;
using EmployeesAPI.Models.Database;
using EmployeesAPI.Data.Context;
using EmployeesAPI.Domain.Abstractions;

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
            var queryString = "INSERT INTO public.\"Employees\" (Name, Surname, Phone, CompanyId, PassportId, DepartmentId) " +
                "VALUES (@Name, @Surname, @Phone, @CompanyId, @PassportId, @DepartmentId) RETURNING Id";

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
            var queryString = "DELETE FROM public.\"Employees\" WHERE Id = @Id;";

            await _context.ExecuteAsync(queryString, new { Id = id });
        }

        public async Task<IEnumerable<EmployeeShortInfo>> GetAllByCompanyId(int id)
        {
            var queryString = "SELECT empl.Id, empl.Name, empl.Surname, " +
                "empl.Phone, empl.CompanyId, requiredPass.Type, " +
                "requiredPass.Number, requiredDep.DepartmentName, requiredDep.DepartmentPhone FROM public.\"Employees\" empl " +
                "JOIN public.\"Departments\" requiredDep ON requiredDep.Id = empl.DepartmentId " +
                "JOIN public.\"Passports\" requiredPass ON requiredPass.Id = empl.PassportId " +
                "WHERE empl.CompanyId = @Id;";

            return await _context.QueryAsync<EmployeeShortInfo>(queryString, new { Id = id });
        }

        public async Task Update(Employee item)
        {
            var queryUpdEmployee = "UPDATE public.\"Employees\" SET " +
                "Name = CASE WHEN @Name IS NULL THEN Name ELSE @Name END," +
                "Surname = CASE WHEN @Surname IS NULL THEN Surname ELSE @Surname END, " +
                "Phone = CASE WHEN @Phone IS NULL THEN Phone ELSE @Phone END " +
                "WHERE Id = @Id;";

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
            var queryString = "SELECT * FROM public.\"Employees\" " +
                        "WHERE Id = @Id;";
            return await _context.QueryFirstOrDefaultAsync<Employee>(queryString, new { Id = employeeId });
        }

        public async Task<int?> GetDepartmentId(int employeeId)
        {
            var queryString = "SELECT DepartmentId FROM public.\"Employees\" " +
                        "WHERE Id = @Id;";
            return await _context.QueryFirstOrDefaultAsync<int?>(queryString, new { Id = employeeId }); ;
        }

        public async Task<int?> GetPassportId(int employeeId)
        {
            var queryString = "SELECT PassportId FROM public.\"Employees\" " +
                        "WHERE Id = @Id;";
            return await _context.QueryFirstOrDefaultAsync<int?>(queryString, new { Id = employeeId }); ;
        }

        public async Task<IEnumerable<EmployeeShortInfo>> GetAllByDepartmentName(string depName)
        {
            var queryString = "SELECT empl.Id, empl.Name, empl.Surname, " +
                "empl.Phone, empl.CompanyId, requiredPass.Type, " +
                "requiredPass.Number, requiredDep.DepartmentName, requiredDep.DepartmentPhone FROM public.\"Employees\" empl " +
                "JOIN public.\"Departments\" requiredDep ON requiredDep.Id = empl.DepartmentId " +
                "JOIN public.\"Passports\" requiredPass ON requiredPass.Id = empl.PassportId " +
                "WHERE requiredDep.DepartmentName = @depname;";
            return await _context.QueryAsync<EmployeeShortInfo>(queryString, new { depname = depName });
        }

    }
}
