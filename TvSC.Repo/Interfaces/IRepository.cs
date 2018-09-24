using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TvSC.Data.DbModels;

namespace TvSC.Repo.Interfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<T> GetByAsync(Expression<Func<T, bool>> getBy, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAllBy(Expression<Func<T, bool>> getBy, params Expression<Func<T, object>>[] includes);
        Task<bool> ExistAsync(Expression<Func<T, bool>> getBy, params Expression<Func<T, object>>[] includes);
        Task LoadRelatedCollection<TInclude>(T entity, Expression<Func<T, IEnumerable<TInclude>>> collection,
            params Expression<Func<TInclude, object>>[] includes) where TInclude : BaseModel;
        Task LoadRelatedCollectionThenIncludeCollection<TInclude, TIncluded, TThenInclude>(T entity, Expression<Func<T, IEnumerable<TInclude>>> collection,
            Expression<Func<TInclude, TIncluded>> include, Expression<Func<TIncluded, IEnumerable<TThenInclude>>> thenInclude)
            where TInclude : BaseModel where TThenInclude : BaseModel where TIncluded : BaseModel;
        Task LoadRelatedCollectionThenIncludeCollection<TInclude, TIncluded, TThenInclude>(T entity, Expression<Func<T, IEnumerable<TInclude>>> collection1,
            Expression<Func<TInclude, IEnumerable<TIncluded>>> collection2, Expression<Func<TIncluded, TThenInclude>> thenInclude)
            where TInclude : BaseModel where TThenInclude : BaseModel where TIncluded : BaseModel;
        Task<bool> IsEmptyAsync();
        Task<bool> Remove(T entity);
    }
}