using DevFreela.Application.Commands.ProjectCommands.Create;
using DevFreela.Application.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblyContaining<CreateProjectCommand>());

        services.AddTransient<IPipelineBehavior<CreateProjectCommand, ResultViewModel<int>>, ValidateCreateProjectCommandBehavior>();

        return services;
    }
}