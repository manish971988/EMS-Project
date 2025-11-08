using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS.API.Models;

[ApiController]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    private readonly EMSContext _context;
    
    public RoleController(EMSContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _context.Roles.ToListAsync();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        
        if (role == null)
            return NotFound();
        
        return Ok(role);
    }
}
