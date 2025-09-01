using Microsoft.EntityFrameworkCore;
using Tech_task_back_end.Application.Repositories;
using Tech_task_back_end.Domain.Entities;

namespace Tech_task_back_end.Infrastructure.Data.Repositories;

public class ClientRepository(ApplicationDbContext context) : IClientRepository
{
    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await context.Clients.ToListAsync();
    }

    public async Task<bool> AddAsync(Client client)
    {
        var allClients = await context.Clients.ToListAsync();

        if (await context.Clients.FindAsync(client.Id) != null)
        {
            return false;
        }
        
        await context.Clients.AddAsync(client);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Client client)
    {
        context.Clients.Update(client);
        
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await context.Clients.FindAsync(id);
    }

    public async Task<bool> RemoveAsync(Client client)
    {
        context.Clients.Remove(client);
        
        return await context.SaveChangesAsync() > 0;
    }
}