using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Slackers.Services.Repository.Lite
{
    public static class Extensions
    {
        public static IServiceCollection AddLiteRepository(this IServiceCollection services, IConfiguration config, string section)
        {
            services.Configure<LiteRepositoryOptions>(config.GetSection(section));
            services.AddTransient<IRepository, LiteRepository>();
            return services;
        }
    }
}