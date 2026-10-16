using EmployeeCrud.Api.Data;
using EmployeeCrud.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrud.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll(CancellationToken cancellationToken)
        => Ok(await db.Employees.AsNoTracking().OrderBy(e => e.Id).ToListAsync(cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Employee>> GetById(int id, CancellationToken cancellationToken)
    {
        var employee = await db.Employees.FindAsync([id], cancellationToken);
        return employee is null ? NotFound() : Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(Employee employee, CancellationToken cancellationToken)
    {
        employee.Id = 0;
        db.Employees.Add(employee);
        await db.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Employee input, CancellationToken cancellationToken)
    {
        if (id != input.Id) return BadRequest("Route ID must match employee ID.");
        var employee = await db.Employees.FindAsync([id], cancellationToken);
        if (employee is null) return NotFound();

        employee.Name = input.Name;
        employee.Email = input.Email;
        employee.Department = input.Department;
        employee.Salary = input.Salary;
        await db.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var employee = await db.Employees.FindAsync([id], cancellationToken);
        if (employee is null) return NotFound();
        db.Employees.Remove(employee);
        await db.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
