using System;
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
        Task<bool> IsEmptyAsync();
        Task<bool> Remove(T entity);
    }
}