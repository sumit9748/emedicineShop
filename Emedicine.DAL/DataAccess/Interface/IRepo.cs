using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Emedicine.DAL.DataAccess.Interface
{
    public interface IRepo<T> where T : class
    {
        void AddAsync(Task entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void UpdateExisting(T entity);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> GetAllListAsync(Expression<Func<T, bool>> filter);
    }
}
