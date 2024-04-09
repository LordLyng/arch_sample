using Application.CQRS.Employees.Commands;

using Ardalis.ApiEndpoints;

using Domain;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace API.REPR.Employees;

public class Create : EndpointBaseAsync
    .WithRequest<CreateEmployee.Command>
    .WithResult<Employee>
{
    private readonly IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("repr/[namespace]")]
    public override async Task<Employee> HandleAsync(CreateEmployee.Command request, CancellationToken cancellationToken = new CancellationToken())
    {
        return await _mediator.Send(request, cancellationToken);
    }
}