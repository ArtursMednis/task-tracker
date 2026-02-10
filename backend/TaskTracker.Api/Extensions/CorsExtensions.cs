namespace TaskTracker.Api.Extensions
{
    public static class CorsExtensions
    {
        const string LocalhostPolicyName = "AllowLocalhost";
        const string LocalhostOrigin = "http://localhost:4200";
            //"http://localhost:5173";

        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        LocalhostPolicyName,
                        policy =>
                            policy.WithOrigins(LocalhostOrigin)
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials());
                });
        }

        public static void ConfigureCors(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseCors(LocalhostPolicyName);
            }
        }
    }
}
