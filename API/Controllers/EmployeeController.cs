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
            string.IsNullOrWhiteSpace(employeeDto.Department) ||
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
            Department = employeeDto.Department,
            Designation = employeeDto.Designation,
            Salary = employeeDto.Salary,
            Address = employeeDto.Address,
            Status = statusEnum,
            CreatedBy = userId.Value,
            CreatedAt = DateTime.UtcNow,
            UpdatedBy = userId.Value,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Employee added successfully." });
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
    public string Department { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // Accepts "Active", "Inactive", "Deleted"
}
