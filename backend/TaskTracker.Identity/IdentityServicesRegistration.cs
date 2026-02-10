using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskTracker.Application.Contracts.Identity;
using TaskTracker.Identity.DatabaseContext;
using TaskTracker.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TaskTracker.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var idenityDbConnectionString = configuration.GetConnectionString("TaskTrackerIdentityDatabase");

            services.AddDbContext<TasksIdentityDbContext>(options =>
            {
                options.UseSqlServer(idenityDbConnectionString);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = false;
            })
                .AddEntityFrameworkStores<TasksIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ICurrentUser, HttpCurrentUser>();

            return services;
        }
    }
}
