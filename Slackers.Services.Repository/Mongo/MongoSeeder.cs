using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Slackers.Services.Repository.MongoDb;

namespace Slackers.Services.Repository.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase Database;

        public MongoSeeder(IMongoDatabase database)
        {
            this.Database = database;
        }

        public async Task SeedAsync()
        {
            var collectionCursor = await this.Database.ListCollectionsAsync();
            var collections = await collectionCursor.ToListAsync();
            if (collections.Any())
            {
                return;
            }

            await this.CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}