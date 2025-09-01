using Microsoft.EntityFrameworkCore;
using Tech_task_back_end.Application.Repositories;
using Tech_task_back_end.Domain.Entities;

namespace Tech_task_back_end.Infrastructure.Data.Repositories;

public class StatusUpdateRepository(ApplicationDbContext context) : IStatusUpdateRepository
{
    public async Task<IEnumerable<StatusUpdate>> GetAllAsync()
    {
        return await context.StatusUpdates.Include(a => a.Package).ToListAsync();
    }

    public async Task<StatusUpdate?> GetByIdAsync(int id)
    {
        return await context.StatusUpdates.FindAsync(id);
    }

    public async Task<bool> AddAsync(StatusUpdate status)
    {
        await context.StatusUpdates.AddAsync(status);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(StatusUpdate status)
    {
        context.StatusUpdates.Update(status);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveAsync(StatusUpdate status)
    {
        context.StatusUpdates.Remove(status);

        return await context.SaveChangesAsync() > 0;
    }
}