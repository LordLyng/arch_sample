using Application.CQRS.Employees.Commands;
using Application.CQRS.Employees.Queries;

using Domain;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CQRSController : ControllerBase
{
    private readonly IMediator _mediator;

    public CQRSController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllEmployees.Query()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetById([FromRoute] Guid id)
    {
        try
        {
            return Ok(await _mediator.Send(new GetEmployeeById.Query() { Id = id }));
        }
        //TODO: Should specifically catch the domain exception for not found
        //TODO: Explain global exception handlers/filters
        catch (Exception e)
        {
            return NotFound(e);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(CreateEmployee.Command request)
    {
        return Ok(await _mediator.Send(request));
    }
}