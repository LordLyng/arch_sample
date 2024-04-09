using Application.Common;

using Domain;

using FluentValidation;

using MediatR;

namespace Application.CQRS.Employees.Commands;

public class CreateEmployee
{
    public class Command : IRequest<Employee>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly BirthDate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(r => r.FirstName).NotEmpty();
            RuleFor(r => r.LastName).NotEmpty();
            RuleFor(r => r.BirthDate).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Employee>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new Employee()
            {
                BirthDate = request.BirthDate,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            await _dbContext.Employees.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
