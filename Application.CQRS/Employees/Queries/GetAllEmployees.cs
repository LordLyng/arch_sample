using Application.Common;

using Domain;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Employees.Queries;

public class GetAllEmployees
{
    public class Query : IRequest<IEnumerable<Employee>>
    { }

    public class Handler : IRequestHandler<Query, IEnumerable<Employee>>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _dbContext.Employees.ToListAsync(cancellationToken);
        }
    }
}