using Application.CQRS.Employees.Queries;

using Ardalis.ApiEndpoints;

using Domain;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace API.REPR.Employees;

public class GetById : EndpointBaseAsync
    .WithRequest<Guid>
    .WithResult<Employee>
{
    private readonly IMediator _mediator;

    public GetById(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("repr/[namespace]/{id}", Name = "[namespace]_[controller]")]
    public override async Task<Employee> HandleAsync([FromRoute] Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
        return await _mediator.Send(new GetEmployeeById.Query() { Id = id }, cancellationToken);
    }
}