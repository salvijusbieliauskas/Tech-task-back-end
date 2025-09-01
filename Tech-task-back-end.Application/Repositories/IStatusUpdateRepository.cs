using Tech_task_back_end.Domain.Entities;

namespace Tech_task_back_end.Application.Repositories;

public interface IStatusUpdateRepository
{
    Task<IEnumerable<StatusUpdate>> GetAllAsync();
    Task<StatusUpdate?> GetByIdAsync(int id);
    Task<bool> AddAsync(StatusUpdate status);
    Task<bool> UpdateAsync(StatusUpdate status);
    Task<bool> RemoveAsync(StatusUpdate person);
}