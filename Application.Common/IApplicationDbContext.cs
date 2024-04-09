using Domain;

using Microsoft.EntityFrameworkCore;

namespace Application.Common;

public interface IApplicationDbContext
{
    DbSet<Employee> Employees { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
