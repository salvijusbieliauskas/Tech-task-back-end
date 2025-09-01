using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tech_task_back_end.Application.DTOs;
using Tech_task_back_end.Application.Functions.Clients;
using Tech_task_back_end.Application.Functions.Packages;
using Tech_task_back_end.Application.Functions.Packagse;
using Tech_task_back_end.Application.Repositories;
using Tech_task_back_end.Domain.Entities;
using Tech_task_back_end.Domain.Enums;
using Tech_task_back_end.Domain.Helpers;
using Tech_task_back_end.Infrastructure.Data;
using Tech_task_back_end.Infrastructure.Data.Repositories;

namespace Tech_task_back_end.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddEntityFrameworkInMemoryDatabase()
            .AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseInMemoryDatabase("db1").UseInternalServiceProvider(sp);
            });
        
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IPackageRepository, PackageRepository>();
        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<IStatusUpdateRepository, StatusUpdateRepository>();

        return services;
    }

    public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
    {
        services.AddTransient<IClientRequestHandler, ClientRequestHandler>();
        services.AddTransient<IPackageRequestHandler, PackageRequestHandler>();

        return services;
    }

    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.NewConfig<Package, PackageDetailsDto>().Map(a => a.AllowedTransitions,
            b => b.Status.GetAllowedTransitions());

        config.NewConfig<Package, PackagePreviewDto>().Map(a => a.SenderName, b => b.Sender.Name)
            .Map(a => a.RecipientName, b => b.Recipient.Name)
            .Map(a => a.AllowedTransitions, b => b.Status.GetAllowedTransitions());

        config.NewConfig<Status, StatusDto>().Map(a => a.StatusString, b => Enum.GetName(b))
            .Map(a => a.Status, b => b);
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}