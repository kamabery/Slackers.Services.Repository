using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Slackers.Services.Repository.MongoDb
{
    public static class Extension
    {
        /// <summary>
        /// Add Mongo DB Support
        /// </summary>
        /// <param name="services">Services being used</param>
        /// <param name="configuration">Configurtion of services</param>
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection("mongo"));
            services.AddSingleton<MongoClient>(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();
                var client = new MongoClient(options.Value.ConnectionString);
                return client;
            });

            services.AddScoped<IMongoDatabase>(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();
                var client = c.GetService<MongoClient>();

                return client.GetDatabase(options.Value.Database);
            });

            services.AddScoped<IDatabaseInitializer, MongoInitializer>();
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();
            services.AddScoped<IRepository, MongoRepository>();
        }

    }
}