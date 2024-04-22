using EmployeesAPI.Data.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System.Data;

namespace EmployeesAPI.Data.Context
{
    public class DapperContext : IDataContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public async Task ExecuteAsync(string queryString, object parameters)
        {
            await ExecuteDbQuery(query => 
                query.ExecuteAsync(queryString, parameters));
        }

        public async Task<T> QueryFirstAsync<T>(string queryString, object parameters)
        {
            return await ExecuteDbQuery(query => 
                query.QueryFirstAsync<T>(queryString, parameters));
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string queryString, object parameters)
        {
            return await ExecuteDbQuery(query =>
                query.QueryFirstOrDefaultAsync<T>(queryString, parameters));
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string queryString, object parameters)
        {
            return await ExecuteDbQuery(query =>
                query.QueryAsync<T>(queryString, parameters));
        }
        private async Task<T> ExecuteDbQuery<T>(Func<IDbConnection, Task<T>> dbQuery)
        {
            await using (var connection = new NpgsqlConnection(_connectionString))
            {
                var result = await dbQuery(connection);
                await connection.CloseAsync();
                return result;
            }
        }
    }
}
