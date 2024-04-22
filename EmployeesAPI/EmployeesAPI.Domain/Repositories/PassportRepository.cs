using Dapper;
using EmployeesAPI.Data.Context;
using EmployeesAPI.Domain.Abstractions;
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
            var queryString = "INSERT INTO public.\"Passports\" (Type, Number) VALUES (@Type, @Number) RETURNING id;";

            return await _context.QueryFirstAsync<int?>(queryString, new { Type = item.Type, Number = item.Number });
        }

        public async Task Delete(int id)
        {
            var queryString = "DELETE FROM public.\"Passports\" WHERE Id = @Id;";

            await _context.ExecuteAsync(queryString, new { Id = id });
        }

        public async Task<Passport?> ReadByNumAsync(string number)
        {
            var queryString = "SELECT * FROM public.\"Passports\" WHERE Number = @Number";
            return await _context.QueryFirstOrDefaultAsync<Passport>(queryString, new { Number = number });
        }

        public async Task Update(Passport item)
        {
            var queryString = "UPDATE public.\"Passports\" SET " +
                    "Number = CASE WHEN @Number IS NULL THEN Number ELSE @Number END, " +
                    "Type = CASE WHEN @Type IS NULL THEN Type ELSE @Type END " +
                    "WHERE Id = @Id;";
            await _context.ExecuteAsync(queryString, new { Id = item.Id, Type = item.Type, Number = item.Number });
        }
    }
}
