using Microsoft.Extensions.DependencyInjection;
using Tech_task_back_end.Domain.Entities;
using Tech_task_back_end.Domain.Enums;
using Tech_task_back_end.Domain.Helpers;

namespace Tech_task_back_end.Infrastructure.Data;

public class DatabaseInitializer(IServiceProvider serviceProvider)
{

    public void Initialize()
    {
        SeedDatabase();
    }
    
    private void SeedDatabase()
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (dbContext.Packages.Any())
        {
            return;
        }

        var clients = new List<Client>
        {
            new Client { Id = 1, Name = "Acme Corporation", Phone = "123-456-7890", Address = "123 Main St, Business City, 12345" },
            new Client { Id = 2, Name = "TechSolutions Inc.", Phone = "234-567-8901", Address = "456 Tech Blvd, Innovation City, 23456" },
            new Client { Id = 3, Name = "Global Shipping Ltd", Phone = "345-678-9012", Address = "789 Shipping Lane, Port City, 34567" },
            new Client { Id = 4, Name = "Quick Deliveries", Phone = "456-789-0123", Address = "101 Speed Road, Fast Town, 45678" },
            new Client { Id = 5, Name = "Retail Giants", Phone = "567-890-1234", Address = "202 Market Ave, Commerce City, 56789" },
            new Client { Id = 6, Name = "Creative Studios", Phone = "678-901-2345", Address = "303 Art Street, Design District, 67890" },
            new Client { Id = 7, Name = "Healthcare Products", Phone = "789-012-3456", Address = "404 Wellness Way, Health City, 78901" },
            new Client { Id = 8, Name = "Food Distributors", Phone = "890-123-4567", Address = "505 Harvest Road, Foodie Town, 89012" }
        };

        dbContext.Clients.AddRange(clients);

        var random = new Random();
        var packages = new List<Package>();
        var statusUpdates = new List<StatusUpdate>();

        for (int i = 1; i <= 15; i++)
        {
            int senderIndex = random.Next(clients.Count);
            int recipientIndex;
            do
            {
                recipientIndex = random.Next(clients.Count);
            } while (recipientIndex == senderIndex);

            string trackingNumber = TrackingNumber.Create(i);

            DateTime created = DateTime.Now.AddDays(-random.Next(1, 30));

            var package = new Package
            {
                Id = i,
                TrackingNumber = trackingNumber,
                Status = Status.Created,
                Sender = clients[senderIndex],
                Recipient = clients[recipientIndex],
                Created = created
            };
            
            packages.Add(package);

            var statusUpdate = new StatusUpdate(i, created, package, Status.Created);
            
            statusUpdates.Add(statusUpdate);
        }

        dbContext.Packages.AddRange(packages);
        dbContext.StatusUpdates.AddRange(statusUpdates);
        dbContext.SaveChanges();
    }
}