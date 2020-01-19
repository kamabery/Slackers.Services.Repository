using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using Slackers.Services.Repository.MongoDb;

namespace Slackers.Services.Repository.Mongo
{
    public class MongoInitializer: IDatabaseInitializer
    {
        private readonly IDatabaseSeeder _seeder;
        private readonly bool _seed;
        private bool _initialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoInitializer"/> class.
        /// </summary>
        /// <param name="options">Options for initilzation</param>
        /// <param name="seeder">the database seeder</param>
        public MongoInitializer(IOptions<MongoOptions> options, IDatabaseSeeder seeder)
        {
            this._seeder = seeder;
            this._seed = options.Value.Seed;
        }

        /// <summary>
        /// Initialze database
        /// </summary>
        /// <returns>Async task completion</returns>
        public async Task InitializeAsync()
        {
            if (this._initialized)
            {
                return;
            }

            this._initialized = true;
            this.RegisterConventions();

            if (!this._seed)
            {
                return;
            }

            await this._seeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActioConventions", new MongoConvention(), x => true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
