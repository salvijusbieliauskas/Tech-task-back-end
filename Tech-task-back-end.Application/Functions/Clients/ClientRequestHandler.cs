using MapsterMapper;
using Tech_task_back_end.Application.DTOs;
using Tech_task_back_end.Application.Repositories;
using Tech_task_back_end.Domain.Entities;

namespace Tech_task_back_end.Application.Functions.Clients;

public class ClientRequestHandler(IClientRepository clientRepository, IMapper mapper) : IClientRequestHandler
{
    public async Task<IEnumerable<Client>> GetClientsAsync()
    {
        var clients = await clientRepository.GetAllAsync();
        return clients;
    }

    public async Task<int> AddClientAsync(ClientCreateDto client)
    {
        if (string.IsNullOrWhiteSpace(client.Address))
        {
            return -1;
        }

        if (string.IsNullOrWhiteSpace(client.Phone))
        {
            return -1;
        }

        if (string.IsNullOrWhiteSpace(client.Name))
        {
            return -1;
        }

        var clients = await clientRepository.GetAllAsync();
        int id;
        if (clients.Any())
        {
            id = clients.MaxBy(a => a.Id).Id + 1;
        }
        else
        {
            id = 1;
        }

        return await clientRepository.AddAsync(new Client(id, client.Address, client.Name, client.Phone)) ? id : -1;
    }
}