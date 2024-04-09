using Application.Service.Employees.Interfaces;
using Application.Service.Employees.Services;

using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddServiceApplication(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddValidatorsFromAssemblyContaining<EmployeeService>();

        return services;
    }
}