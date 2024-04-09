using Application.Common;
using Application.Service.Employees.Interfaces;
using Application.Service.Employees.Models;

using Domain;

using Microsoft.EntityFrameworkCore;

namespace Application.Service.Employees.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IApplicationDbContext _dbContext;

    public EmployeeService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Employees.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Employee> GetEmployeeById(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Employees.FindAsync([id], cancellationToken);
        if (entity == null)
            throw new Exception($"No Employee found matching the id {id}"); //TODO: Should be domain specific exception

        return entity;
    }

    /// <inheritdoc />
    public async Task<Employee> CreateEmployee(CreateEmployeeRequest input, CancellationToken cancellationToken = default)
    {
        var entity = new Employee()
        {
            BirthDate = input.BirthDate, FirstName = input.FirstName, LastName = input.LastName
        };

        await _dbContext.Employees.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}