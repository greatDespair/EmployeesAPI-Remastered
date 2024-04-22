using Dapper;
using EmployeesAPI.Data.Context;
using EmployeesAPI.Domain.Abstractions;
using EmployeesAPI.Domain.Resources;
using EmployeesAPI.Models.Database;

namespace EmployeesAPI.Domain.Repositories
{
    public class PassportRepository : IRepository<Passport>, IPassportRepository
    {
        private readonly DapperContext _context;

        public PassportRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<int?> Add(Passport item)
        {
            var queryString = SqlQueries.PassportInsert;

            return await _context.QueryFirstAsync<int?>(queryString, new { Type = item.Type, Number = item.Number });
        }

        public async Task Delete(int id)
        {
            var queryString = SqlQueries.PassportDelete;

            await _context.ExecuteAsync(queryString, new { Id = id });
        }

        public async Task<Passport?> ReadByNumAsync(string number)
        {
            var queryString = SqlQueries.PassportReadByNum;

            return await _context.QueryFirstOrDefaultAsync<Passport>(queryString, new { Number = number });
        }

        public async Task Update(Passport item)
        {
            var queryString = SqlQueries.PassportUpdate;

            await _context.ExecuteAsync(queryString, new { Id = item.Id, Type = item.Type, Number = item.Number });
        }
    }
}
