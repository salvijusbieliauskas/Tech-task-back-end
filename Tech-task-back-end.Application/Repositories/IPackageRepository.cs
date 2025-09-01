using Tech_task_back_end.Domain.Entities;

namespace Tech_task_back_end.Application.Repositories;

public interface IPackageRepository
{
    Task<IEnumerable<Package>> GetAllAsync();
    Task<Package?> GetByIdAsync(int id);
    Task<Package?> GetByTrackingNumberAsync(string trackingNumber);
    Task<Package?> AddAsync(Package package);
    Task<bool> UpdateAsync(Package package);
    Task<bool> RemoveAsync(Package package);
}