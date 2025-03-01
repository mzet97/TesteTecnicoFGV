using DesafioFGV.Domain.Repositories;
using DesafioFGV.Domain.Services.Interface;
using DesafioFGV.Infrastructure.Persistence;
using DesafioFGV.Infrastructure.Persistence.Repositories;
using DesafioFGV.Infrastructure.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioFGV.Infrastructure.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependenciesInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<DbContext, ApplicationDbContext>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IRedisService, RedisService>();

        return services;
    }
}