using TaskTracker.Api.Extensions;
using TaskTracker.Api.Middleware;
using TaskTracker.Application;
using TaskTracker.Identity;
using TaskTracker.Infrastructure;
using TaskTracker.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseInfrastructureSerilog();

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCorsPolicy();
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();


app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.ConfigureSwaggerUI();
app.ConfigureCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
