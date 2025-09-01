using Microsoft.EntityFrameworkCore;
using Tech_task_back_end.Application.Repositories;
using Tech_task_back_end.Domain.Entities;

namespace Tech_task_back_end.Infrastructure.Data.Repositories;

public class PackageRepository(ApplicationDbContext context) : IPackageRepository
{
    public async Task<IEnumerable<Package>> GetAllAsync()
    {
        return await context.Packages.Include(a => a.Recipient).Include(a => a.Sender)
            .ToListAsync();
    }

    public async Task<Package?> GetByIdAsync(int id)
    {
        return await context.Packages.FindAsync(id);
    }

    public async Task<Package?> GetByTrackingNumberAsync(string trackingNumber)
    {
        return await context.Packages.Include(a => a.Sender).Include(a => a.Recipient).FirstOrDefaultAsync(a =>
            a.TrackingNumber.Equals(trackingNumber, StringComparison.InvariantCultureIgnoreCase));
    }

    public async Task<Package?> AddAsync(Package package)
    {
        await context.Packages.AddAsync(package);

        return await context.SaveChangesAsync() > 0 ? package : null;
        ;
    }

    public async Task<bool> UpdateAsync(Package package)
    {
        context.Packages.Update(package);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveAsync(Package package)
    {
        context.Packages.Remove(package);

        return await context.SaveChangesAsync() > 0;
    }
}