using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Domain.TaskItems;
using TaskTracker.Persistance.DatabaseContext;
using TaskTracker.Persistance.Repositories;

namespace TaskTracker.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var taskDbConnectionString = configuration.GetConnectionString("TaskTrackerDatabase");

            services.AddDbContext<TasksDbContext>(options => {
                options.UseSqlServer(taskDbConnectionString);
            });
            services.AddScoped<ITaskRepository, TaskRepository>();
            return services;
        }
    }
}
