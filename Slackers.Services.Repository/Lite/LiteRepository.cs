using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Slackers.Services.Repository.Lite
{
    public class LiteRepository : IRepository
    {
        private readonly string _connectionString;

        public LiteRepository(IOptions<LiteRepositoryOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public async Task<IEnumerable<T>> Get<T>()
        {
            return await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                return db.Query<T>().ToList();
            });

        }

        public async Task<IEnumerable<T>> Get<T>(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                return db.Query<T>().Where(predicate).ToList();
            });
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> predicate, bool isNullable)
        {
            if (isNullable)
            {
                return await Task.Run(() =>
                {
                    using var db = new LiteDB.LiteRepository(_connectionString);
                    return db.Query<T>().Where(predicate).FirstOrDefault();
                });
            }

            return await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                return db.Query<T>().Where(predicate).Single();
            });
        }

        public async Task<T> Get<T>(Guid id, bool isNullable = true) where T : IEntity
        {
            return await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                if (isNullable)
                {
                    return db.Query<T>().Where(e => e.Id == id).FirstOrDefault();
                }

                return db.Query<T>().Where(e => e.Id == id).Single();
            });
        }

        public async Task Post<T>(T entity)
        {
            await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                db.Insert(entity);
            });
        }

        public async Task Put<T>(T entity) where T : IEntity
        {
            await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                db.Update(entity);
            });
        }

        public async Task Delete<T>(Guid id) where T : IEntity
        {
            await Task.Run(() =>
            {
                using var db = new LiteDB.LiteRepository(_connectionString);
                db.Delete<T>(e => e.Id == id);
            });
        }
    }
}
