using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TvSC.Data.DbModels;
using TvSC.Repo.Interfaces;

namespace TvSC.Repo.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly DataContext _dbContext;
        private DbSet<T> _dbSet;

        public Repository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveAsync();

        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveAsync();
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> getBy, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(getBy);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public IQueryable<T> GetAllBy(Expression<Func<T, bool>> getBy, params Expression<Func<T, object>>[] includes)
        {
            return GetAll(includes).Where(getBy);
        }
        

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> getBy, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.AnyAsync(getBy);
        }

        public async Task LoadRelatedCollection<TInclude>(T entity, Expression<Func<T, IEnumerable<TInclude>>> collection,
            params Expression<Func<TInclude, object>>[] includes) where TInclude : BaseModel
        {
            var query = _dbContext.Entry(entity).Collection(collection).Query();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            await query.LoadAsync();
        }

        public async Task LoadRelatedCollectionThenIncludeCollection<TInclude, TIncluded, TThenInclude>(T entity, Expression<Func<T, IEnumerable<TInclude>>> collection,
            Expression<Func<TInclude, TIncluded>> include, Expression<Func<TIncluded, IEnumerable<TThenInclude>>> thenInclude)
            where TInclude : BaseModel where TThenInclude : BaseModel where TIncluded : BaseModel
        {
            await _dbContext.Entry(entity).Collection(collection).Query().Include(include).ThenInclude(thenInclude).LoadAsync();

        }

        public async Task LoadRelatedCollectionThenIncludeCollection<TInclude, TIncluded, TThenInclude>(T entity, Expression<Func<T, IEnumerable<TInclude>>> collection1,
            Expression<Func<TInclude, IEnumerable<TIncluded>>> collection2, Expression<Func<TIncluded, TThenInclude>> thenInclude)
            where TInclude : BaseModel where TThenInclude : BaseModel where TIncluded : BaseModel
        {
            await _dbContext.Entry(entity).Collection(collection1).Query().OfType<TInclude>().Include(collection2).ThenInclude(thenInclude).LoadAsync();
        }

        public async Task<bool> IsEmptyAsync()
        {
            return await _dbSet.AnyAsync();
        }

        public async Task<bool> Remove(T entity)
        {
            _dbSet.Remove(entity);
            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
