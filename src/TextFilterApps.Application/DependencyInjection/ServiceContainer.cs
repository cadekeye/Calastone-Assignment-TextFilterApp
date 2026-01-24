using Microsoft.Extensions.DependencyInjection;
using TextFilterApps.Application.Contracts;
using TextFilterApps.Application.Services;

namespace TextFilterApps.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<ITextFilterProcessor, TextFilterProcessor>();
            return services;
        }
    }
}