using Auction.Core.Repository.Common.Entity;
using Auction.Core.Repository.Common.Interface.BaseEntity;
using System.Linq.Expressions;

namespace Auction.Core.Repository.Common.Interface.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class, IBaseEntity
    {
        IQueryable<TEntity> GetAll(FindingOptions? findOptions = null);
        TEntity? FindOne(Expression<Func<TEntity, bool>> predicate, FindingOptions? findOptions = null);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindingOptions? findOptions = null);
        void Add(TEntity entity);
        void AddMany(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteMany(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
    }
}
