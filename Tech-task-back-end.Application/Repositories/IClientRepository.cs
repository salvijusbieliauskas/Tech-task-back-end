using Tech_task_back_end.Domain.Entities;

namespace Tech_task_back_end.Application.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<bool> AddAsync(Client client);
    Task<bool> UpdateAsync(Client client);
    Task<Client?> GetByIdAsync(int id);
    Task<bool> RemoveAsync(Client client);
}