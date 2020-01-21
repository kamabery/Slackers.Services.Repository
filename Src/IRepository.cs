using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Slackers.Services.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<T>> Get<T>();
        Task<IEnumerable<T>> Get<T>(Expression<Func<T, bool>> predicate);
        Task<T> Get<T>(Expression<Func<T, bool>> predicate, bool isNullable);
        Task<T> Get<T>(Guid id, bool isNullable = true) where T : IEntity;
        Task Post<T>(T entity);
        Task Put<T>(T entity) where T : IEntity;
        Task Delete<T>(Guid id) where T : IEntity;

    }
}