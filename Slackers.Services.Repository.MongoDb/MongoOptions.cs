namespace Slackers.Services.Repository.MongoDb
{
    public class MongoOptions
    {
        /// <summary>
        /// Gets or sets Connection string for database
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets Database name
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to seed the database
        /// </summary>
        public bool Seed { get; set; }

    }
}