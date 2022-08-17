using Csi.Ems.Api.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Csi.Ems.Api.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<bool> IsExistingAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> IsExistingAsync(TEntity entity);

        Task<TEntity> GetAsync(params object[] keyValues);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
