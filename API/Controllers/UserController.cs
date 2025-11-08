using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS.API.Models;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly EMSContext _context;
    
    public UserController(EMSContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users
            .Include(u => u.Role)
            .ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
        
        if (user == null)
            return NotFound();
        
        return Ok(user);
    }
}
