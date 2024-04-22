namespace EmployeesAPI.Domain.Abstractions
{
    public interface IRepository<T>
    {
        Task<int?> Add(T item);

        Task Update(T item);

        Task Delete(int id);
    }
}
