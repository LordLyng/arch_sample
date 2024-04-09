using Application.CQRS.Common.Behaviours;
using Application.CQRS.Employees.Queries;

using FluentValidation;

using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddCQRSApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<GetAllEmployees.Query>();
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });
        services.AddValidatorsFromAssemblyContaining<GetAllEmployees.Query>();

       return services;
    }
}