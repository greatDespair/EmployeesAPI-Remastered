using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesAPI.Data.Abstractions
{
    public interface IDataContext
    {
        Task<T?> QueryFirstOrDefaultAsync<T>(string queryString, object parameters);
        Task<T> QueryFirstAsync<T>(string queryString, object parameters);
        Task<IEnumerable<T>> QueryAsync<T>(string queryString, object parameters);
        Task ExecuteAsync(string queryString, object parameters);
    }
}
