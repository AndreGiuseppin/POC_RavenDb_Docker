using POC_RavenDb_Docker.Clients.RavenClient;
using POC_RavenDb_Docker.Clients.RavenClient.Interfaces;
using POC_RavenDb_Docker.Clients.RavenClient.Services;
using POC_RavenDb_Docker.Services;
using POC_RavenDb_Docker.Services.Interfaces;

namespace POC_RavenDb_Docker.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRavenService, RavenService>();
            services.AddScoped<IBookService, BookService>();
            services.AddSingleton<IDocumentStoreHolder, DocumentStoreHolder>();

            return services;
        }
    }
}
