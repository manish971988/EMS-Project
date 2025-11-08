using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS.API.Models;

[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly EMSContext _context;

    public DepartmentController(EMSContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await _context.Departments.ToListAsync();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return NotFound();
        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
    {
        if (string.IsNullOrWhiteSpace(departmentDto.Name))
        {
            return BadRequest("Department name is required.");
        }

        var department = new Department
        {
            Name = departmentDto.Name
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Department added successfully.", id = department.Id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return NotFound();

        if (string.IsNullOrWhiteSpace(departmentDto.Name))
        {
            return BadRequest("Department name is required.");
        }

        department.Name = departmentDto.Name;
        await _context.SaveChangesAsync();
        return Ok(new { message = "Department updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null) return NotFound();

        // Check if any employees are using this department
        var hasEmployees = await _context.Employees.AnyAsync(e => e.DepartmentId == id);
        if (hasEmployees)
        {
            return BadRequest("Cannot delete department that has employees assigned to it.");
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Department deleted successfully." });
    }
}

public class DepartmentDto
{
    public string Name { get; set; } = string.Empty;
}
