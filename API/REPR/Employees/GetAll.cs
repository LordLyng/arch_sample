using Application.CQRS.Employees.Queries;

using Ardalis.ApiEndpoints;

using Domain;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace API.REPR.Employees;

public class GetAll : EndpointBaseAsync
    .WithoutRequest
    .WithResult<IEnumerable<Employee>>
{
    private readonly IMediator _mediator;

    public GetAll(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("repr/[namespace]")]
    public override async Task<IEnumerable<Employee>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
       return await _mediator.Send(new GetAllEmployees.Query(), cancellationToken);
    }
}