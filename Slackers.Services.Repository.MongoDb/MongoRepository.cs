using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using System.Reflection;
using MongoDB.Driver.Linq;


namespace Slackers.Services.Repository.MongoDb
{
    public class MongoRepository : IRepository
    {
        private readonly IMongoDatabase _database;
        private readonly ILogger<MongoRepository> _logger;


        public MongoRepository(IMongoDatabase database, ILogger<MongoRepository> logger)
        {
            _database = database;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> Get<T>()
        {
            _logger.LogInformation($"Getting Entity of Type {typeof(T)}");
            return await GetCollection<T>().AsQueryable().ToListAsync();

        }

        public async Task<IEnumerable<T>> Get<T>(Expression<Func<T, bool>> predicate)
        {
            _logger.LogInformation($"Getting first or default entities of type {typeof(T)}");
            return await GetCollection<T>().AsQueryable().Where(predicate).ToListAsync();
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> predicate, bool isNullable)
        {
            if (isNullable)
            {
                this._logger.LogInformation($"Getting first or default entities of type {typeof(T)}");
                return await GetCollection<T>().AsQueryable().Where(predicate).FirstOrDefaultAsync();

            }

            _logger.LogInformation($"Getting single entity of type {typeof(T)}");
            return await GetCollection<T>().AsQueryable().SingleAsync();


        }

        public async Task<T> Get<T>(Guid id, bool isNullable = true) where T : IEntity
        {
            _logger.LogInformation($"Getting entity by Id of type {typeof(T)} by Id {id}");
            return await GetCollection<T>().AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Post<T>(T entity)
        {
            _logger.LogInformation($"Adding entity of type {typeof(T)}");
            try
            {
                await GetCollection<T>().InsertOneAsync(entity);
            }
            catch (Exception ex)
            {
                this._logger.LogCritical(ex, $"Entity Type {typeof(T)}");
            }
        }

        public async Task Put<T>(T entity) where T : IEntity
        {
            _logger.LogInformation($"Updating entity of type {typeof(T)} with Id {entity.Id}");
            await GetCollection<T>().UpdateOneAsync(t => t.Id == entity.Id, new ObjectUpdateDefinition<T>(entity));
        }

        public async Task Delete<T>(Guid id) where T : IEntity
        {
            _logger.LogInformation($"Deleting entity of type {typeof(T)} with Id {id}");
            var result = await this.GetCollection<T>().DeleteOneAsync<T>(x => x.Id == id);

            if (!result.IsAcknowledged)
            {
                _logger.LogError($"Could not delete entity of type {typeof(T)}");
                result.DeletedCount.Equals(null); // this will throw the appropriate MongoDB exception
            }

        }

        private IMongoCollection<T> GetCollection<T>()
        {
            try
            {
                var name = typeof(T).GetCustomAttribute<CollectionName>().Name;
                return this._database.GetCollection<T>(name);
            }
            catch (Exception e)
            {
                this._logger.LogCritical(e, $"Exception for {typeof(T)}");
                throw;
            }
        }
    }
}