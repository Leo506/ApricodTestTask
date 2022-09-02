using Calabonga.OperationResults;

namespace TestTask.Data;

public interface IRepository<T> : IDisposable where T : class
{
    Task<OperationResult<IEnumerable<T>>> GetAllAsync();

    Task<OperationResult<T>> GetAsync(int id);

    Task<OperationResult> CreateAsync(T item);

    Task<OperationResult<bool>> UpdateAsync(T item);

    Task<OperationResult<bool>> DeleteAsync(T item);
}