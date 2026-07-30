using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(
        IEmployeeService service,
        ILogger<EmployeesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        int page = 1,
        int pageSize = 10,
        string? sortBy = null)
    {
        _logger.LogInformation("Getting all employees.");

        var employees = await _service.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            if (sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                employees = employees.OrderBy(e => e.FullName);

            else if (sortBy.Equals("hiredate", StringComparison.OrdinalIgnoreCase))
                employees = employees.OrderBy(e => e.HireDate);
        }

        employees = employees
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Getting employee with ID {Id}", id);

        var employee = await _service.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDto dto)
    {
        var employees = await _service.GetAllAsync();

        if (employees.Any(e => e.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
            return BadRequest("Email already exists.");

        var employee = new Employee
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Department = dto.Department,
            Salary = dto.Salary,
            HireDate = dto.HireDate,
            IsActive = true
        };

        var result = await _service.AddAsync(employee);

        _logger.LogInformation("Employee created with ID {Id}", result.Id);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEmployeeDto dto)
    {
        var employee = await _service.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        employee.FullName = dto.FullName;
        employee.Email = dto.Email;
        employee.Department = dto.Department;
        employee.Salary = dto.Salary;
        employee.HireDate = dto.HireDate;

        await _service.UpdateAsync(employee);

        _logger.LogInformation("Employee updated with ID {Id}", id);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        var employee = await _service.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        await _service.DeleteAsync(id);

        _logger.LogInformation("Employee soft deleted with ID {Id}", id);

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string? name, string? department)
    {
        _logger.LogInformation("Searching employees.");

        var employees = await _service.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(name))
            employees = employees.Where(e =>
                e.FullName.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(department))
            employees = employees.Where(e =>
                e.Department.Contains(department, StringComparison.OrdinalIgnoreCase));

        return Ok(employees);
    }
}