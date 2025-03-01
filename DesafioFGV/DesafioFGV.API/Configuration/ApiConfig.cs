using DesafioFGV.Application.Configuration;
using DesafioFGV.Infrastructure.Configuration;

namespace DesafioFGV.API.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddIdentityConfig(configuration);
        services.AddCorsConfig();
        services.ResolveDependenciesInfrastructure();
        services.ResolveDependenciesApplication();

        services.AddApplicationServices();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerConfig();
        services.AddSwaggerGen();
        services.AddHealthChecks();
        services.AddAuthorization();
        services.AddApiResponseCompression();

        services.AddControllers();

        return services;
    }
}

