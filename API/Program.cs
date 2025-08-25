using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddDbContext<EMS.API.Models.EMSContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0))));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();
