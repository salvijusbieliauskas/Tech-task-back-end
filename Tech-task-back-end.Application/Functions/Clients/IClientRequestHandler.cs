using Tech_task_back_end.Application.DTOs;
using Tech_task_back_end.Domain.Entities;

namespace Tech_task_back_end.Application.Functions.Clients;

public interface IClientRequestHandler
{
    Task<IEnumerable<Client>> GetClientsAsync();
    Task<int> AddClientAsync(ClientCreateDto client);
}