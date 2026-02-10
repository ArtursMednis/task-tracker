using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.Services;

namespace TaskTracker.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<TaskService>();
            return services;
        }
    }
}
