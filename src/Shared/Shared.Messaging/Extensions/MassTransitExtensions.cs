using System.Reflection;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Messaging.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTranssitWithAssemblies(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies)
    {
        services.AddMassTransit(
            config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                config.SetInMemorySagaRepositoryProvider();

                config.AddConsumers(assemblies);
                config.AddSagaStateMachines(assemblies);
                config.AddSagas(assemblies);
                config.AddActivities(assemblies);
                config.AddActivities(assemblies);

                config.UsingRabbitMq(
                    (
                        context,
                        cfg) =>
                    {
                        cfg.Host(
                            configuration["MessageBroker:Host"]!,
                            configurator =>
                            {
                                configurator.Username(configuration["MessageBroker:UserName"]!);
                                configurator.Password(configuration["MessageBroker:Password"]!);
                            });

                        cfg.ConfigureEndpoints(context);
                    });
            });

        return services;
    }
}
