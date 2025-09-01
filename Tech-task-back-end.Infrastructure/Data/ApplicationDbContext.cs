using Microsoft.EntityFrameworkCore;
using Tech_task_back_end.Domain.Entities;

namespace Tech_task_back_end.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Package> Packages { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<StatusUpdate> StatusUpdates { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}