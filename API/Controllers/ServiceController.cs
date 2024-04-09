using Application.Service.Employees.Interfaces;
using Application.Service.Employees.Models;

using Domain;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public ServiceController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
    {
        return Ok(await _employeeService.GetAllEmployees(HttpContext.RequestAborted));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetById([FromRoute] Guid id)
    {
        try
        {
            return Ok(await _employeeService.GetEmployeeById(id, HttpContext.RequestAborted));
        }
        //TODO: Should specifically catch the domain exception for not found
        //TODO: Explain global exception handlers/filters
        catch (Exception e)
        {
            return NotFound(e);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(CreateEmployeeRequest request)
    {
        return Ok(await _employeeService.CreateEmployee(request, HttpContext.RequestAborted));
    }
}