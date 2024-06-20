using API.Extensions;
using API.Middleware;
using Core.Data;
using Infrastructure.Data.Context;
using Infrastructure.Data.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAppServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var context = services.GetRequiredService<DataContext>();

var userManager = services.GetRequiredService<UserManager<AppUser>>();
var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await Seed.SeedAsync(userManager, roleManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

app.Run();