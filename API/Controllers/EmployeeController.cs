using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS.API.Models;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly EMSContext _context;
    public EmployeeController(EMSContext context)
    {
        _context = context;
    }

    [HttpPost]
    // [Authorize(Roles = "Admin,Supervisor")]
    public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
    {
        if (string.IsNullOrWhiteSpace(employeeDto.Name) ||
            employeeDto.DepartmentId <= 0 ||
            string.IsNullOrWhiteSpace(employeeDto.Designation) ||
            employeeDto.Salary <= 0 ||
            string.IsNullOrWhiteSpace(employeeDto.Status))
        {
            return BadRequest("Validation error: Please check all required fields.");
        }

        if (!Enum.TryParse<EmployeeStatus>(employeeDto.Status, true, out var statusEnum))
        {
            return BadRequest("Invalid status value. Use 'Active', 'Inactive', or 'Deleted'.");
        }

        var userId = GetLoggedInUserId(); // Implement this method based on your auth
        if (userId == null)
            return Unauthorized();

        var employee = new Employee
        {
            Name = employeeDto.Name,
            DepartmentId = employeeDto.DepartmentId,
            Designation = employeeDto.Designation,
            Salary = employeeDto.Salary,
            Address = employeeDto.Address,
            Status = statusEnum,
            CreatedBy = userId.Value,
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = userId.Value, // <-- Ensure this is set!
            UpdatedAt = DateTime.UtcNow
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Employee added successfully." });
    }

    [HttpPut("{id}")]
    // [Authorize(Roles = "Admin,Supervisor")]
    public async Task<IActionResult> EditEmployee(int id, [FromBody] EmployeeDto employeeDto)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();

        // Prevent editing deleted employees
        if (employee.Status == EmployeeStatus.Deleted)
            return BadRequest("Cannot edit a deleted employee.");

        // Validation
        if (string.IsNullOrWhiteSpace(employeeDto.Name) ||
            employeeDto.DepartmentId <= 0 ||
            string.IsNullOrWhiteSpace(employeeDto.Designation) ||
            employeeDto.Salary <= 0 ||
            string.IsNullOrWhiteSpace(employeeDto.Status) ||
            !(employeeDto.Status == "Active" || employeeDto.Status == "Inactive"))
        {
            return BadRequest("Invalid input.");
        }

        // Update fields
        employee.Name = employeeDto.Name;
        employee.DepartmentId = employeeDto.DepartmentId;
        employee.Designation = employeeDto.Designation;
        employee.Salary = employeeDto.Salary;
        employee.Address = employeeDto.Address;
        employee.Status = employeeDto.Status == "Active" ? EmployeeStatus.Active : EmployeeStatus.Inactive;
      
        employee.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Employee updated successfully." });
    }

    [HttpPut("{id}/delete")]
    // [Authorize(Roles = "Admin,Supervisor")]
    public async Task<IActionResult> SoftDeleteEmployee(int id, [FromBody] SoftDeleteRequest request)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null || employee.Status == EmployeeStatus.Deleted)
            return NotFound(new { message = "Employee not found or already deleted." });

        employee.Status = EmployeeStatus.Deleted;
        employee.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Optionally: log audit here

        return Ok(new { message = "Employee deleted successfully." });
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees([FromQuery] string status = "")
    {
        List<Employee> employees;

        employees = await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.Status == EmployeeStatus.Active || e.Status == EmployeeStatus.Inactive)
                .ToListAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    // Helper to get logged-in user ID (replace with your auth logic)
    private int? GetLoggedInUserId()
    {
        // Example: return int.Parse(User.FindFirst("sub").Value);
        return 1; // TODO: Replace with real user ID
    }
}

public class EmployeeDto
{
    public string Name { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string Designation { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // Accepts "Active", "Inactive", "Deleted"
}

public class SoftDeleteRequest
{
    public int UpdatedBy
    {
        get; set;
    }
}