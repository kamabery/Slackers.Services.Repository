using System;
using System.Threading.Tasks;
using Mongo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Slackers.Services.Repository.MongoDb;

namespace Mongo
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        private static IConfigurationRoot _configurationRoot;

        static async Task Main()
        {

            RegisterService();
            var service = _serviceProvider.GetService<IMyService>();
            await service.Run();
            DisposeServices();
        }

        private static void RegisterService()
        {
            var collection = new ServiceCollection();
            collection.AddLogging(LoggingBuilder);
            collection.AddScoped<IMyService, MyService>();
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            _configurationRoot = builder.Build();
            collection.AddSingleton(_configurationRoot);
            collection.AddMongoDb(_configurationRoot, "mongo");
            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }


        private static void LoggingBuilder(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
            loggingBuilder.SetMinimumLevel(LogLevel.Information);
        }
    }
}
