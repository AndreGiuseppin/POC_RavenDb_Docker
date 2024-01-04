using Microsoft.OpenApi.Models;

namespace POC_RavenDb_Docker.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerExtensions(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "POC RavenDb With Docker", Version = "v1" });
            });

            return services;
        }
    }
}
