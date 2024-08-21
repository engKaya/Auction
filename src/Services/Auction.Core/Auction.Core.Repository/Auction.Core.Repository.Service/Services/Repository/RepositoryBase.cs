using Auction.Core.Repository.Common.Entity;
using Auction.Core.Repository.Common.Interface.BaseEntity;
using Auction.Core.Repository.Common.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Auction.Core.Repository.Service.Services.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly DbContext _dbContext;
        public RepositoryBase(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Add(TEntity entity)
        { 
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.MinValue;

            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void AddMany(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.MinValue;
            }

            _dbContext.Set<TEntity>().AddRange(entities);
            _dbContext.SaveChanges();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate) =>  _dbContext.Set<TEntity>().Any(predicate);

        public int Count(Expression<Func<TEntity, bool>> predicate) => _dbContext.Set<TEntity>().Count(predicate);

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _dbContext.Set<TEntity>().Where(predicate);
            _dbContext.Set<TEntity>().RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindingOptions? findOptions = null) => Get(findOptions).Where(predicate);

        public TEntity? FindOne(Expression<Func<TEntity, bool>> predicate, FindingOptions? findOptions = null) => Get(findOptions).FirstOrDefault(predicate);

        public IQueryable<TEntity> GetAll(FindingOptions? findOptions = null) => Get(findOptions);
        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        private DbSet<TEntity> Get(FindingOptions? findOptions = null)
        {
            findOptions ??= new FindingOptions();

            var entity = _dbContext.Set<TEntity>();

            if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
                entity.IgnoreAutoIncludes().AsNoTracking();
            else if (findOptions.IsIgnoreAutoIncludes)
                entity.IgnoreAutoIncludes();
            else if (findOptions.IsAsNoTracking) 
                entity.AsNoTracking(); 

            return entity;
        }
    }
}
