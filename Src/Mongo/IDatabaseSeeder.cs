using System.Threading.Tasks;

namespace Slackers.Services.Repository.MongoDb
{
    public interface IDatabaseSeeder
    {
        /// <summary>
        /// Seed Async
        /// </summary>
        /// <returns>Ansync completion</returns>
        Task SeedAsync();

    }
}