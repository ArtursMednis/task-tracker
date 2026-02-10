using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TaskTracker.Application.Contracts.DateAndTime;
using TaskTracker.Infrastructure.DateAndTime;

namespace TaskTracker.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            return services;
        }

        public static void UseInfrastructureSerilog(this IHostBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
             .WriteTo.Console()
             .MinimumLevel.Error()
             .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
             .CreateLogger();

            builder.UseSerilog();
        }
    }
}
