using Application.Common;

using Domain;

using FluentValidation;

using MediatR;

namespace Application.CQRS.Employees.Queries;

public class GetEmployeeById
{
    public class Query : IRequest<Employee>
    {
        public required Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(r => r.Id).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Query, Employee>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> Handle(Query request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Employees.FindAsync([request.Id], cancellationToken);
            if (entity == null)
                throw new Exception($"No Employee found matching the id {request.Id}"); //TODO: Should be domain specific exception

            return entity;
        }
    }
}