using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Slackers.Services.Repository.MongoDb
{
    public class MongoInitializer
    {
        private readonly IDatabaseSeeder seeder;
        private readonly bool seed;
        private bool initialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoInitializer"/> class.
        /// </summary>
        /// <param name="options">Options for initilzation</param>
        /// <param name="seeder">the database seeder</param>
        public MongoInitializer(IOptions<MongoOptions> options, IDatabaseSeeder seeder)
        {
            this.seeder = seeder;
            this.seed = options.Value.Seed;
        }

        /// <summary>
        /// Initialze database
        /// </summary>
        /// <returns>Async task completion</returns>
        public async Task InitializeAsync()
        {
            if (this.initialized)
            {
                return;
            }

            this.initialized = true;
            this.RegisterConventions();

            if (!this.seed)
            {
                return;
            }

            await this.seeder.SeedAsync();
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
