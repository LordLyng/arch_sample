using Application.Service.Employees.Models;

using Domain;

namespace Application.Service.Employees.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken cancellationToken = default);
    Task<Employee> GetEmployeeById(Guid id, CancellationToken cancellationToken = default);
    Task<Employee> CreateEmployee(CreateEmployeeRequest input, CancellationToken cancellationToken = default);
}