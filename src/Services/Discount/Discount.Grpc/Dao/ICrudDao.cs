using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace Discount.Grpc.Dao
{
    public interface ICrudDao<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetByConditions(Expression<Func<T, bool>> expression);

        Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression);

        Task<T> GetById(int id);

        Task<T> GetById(string id);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<T> Delete(T entity);

        Task<T> Delete(int id);

        Task<T> Delete(string id);

        Task DeleteMulti(IEnumerable<T> entities);

        Task AddMulti(IEnumerable<T> entities);

        Task<bool> IsExist(Expression<Func<T, bool>> expression);
    }
}
