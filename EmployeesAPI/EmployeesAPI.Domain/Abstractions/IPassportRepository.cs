using EmployeesAPI.Models.Database;

namespace EmployeesAPI.Domain.Abstractions
{
    public interface IPassportRepository : IRepository<Passport>
    {
        Task<Passport?> ReadByNumAsync(string number); 
    }
}
