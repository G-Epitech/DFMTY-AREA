using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Application.Services.Integrations;
using Zeus.Api.Integration.Mapping;
using Zeus.BuildingBlocks.Application.Behaviors;

namespace Zeus.Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, ServiceLifetime.Singleton);
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));

        services.AddIntegrationMappings();
        services.AddScoped<IIntegrationService, IntegrationService>();
        return services;
    }
}
