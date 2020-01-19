using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Model;
using Slackers.Services.Repository;

namespace Lite.Services
{
    public class MyService : IMyService
    {
        private readonly IRepository _repository;
        private readonly ILogger<MyService> _logger;

        public MyService(ILoggerFactory loggerFactory, IRepository repository)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<MyService>();
        }

        public async Task Run()
        {
            _logger.LogInformation("Adding Geography");
            var geography = new Geography("Fire Swamp", new[] { "Flame Bursts", "Lightning Sand", "ROU" });
            var id = geography.Id;
            await _repository.Post(geography);

            _logger.LogInformation("Getting Information");
            var dbGeography = await _repository.Get<Geography>(geography.Id);
            _logger.LogInformation($"Name {dbGeography.Name}");
            _logger.LogInformation($"Dangers {string.Join(',', dbGeography.Dangers)}");
        }
    }
}