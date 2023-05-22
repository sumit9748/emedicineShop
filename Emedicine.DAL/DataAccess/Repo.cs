using Emedicine.DAL.Data;
using Emedicine.DAL.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Emedicine.DAL.DataAccess
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly MedicineDbContext md;
        public DbSet<T> DbSet { get; set; }
        public Repo(MedicineDbContext _md) 
        {
            md = _md;
            DbSet=_md.Set<T>();
        }

        public void AddAsync(T entity)
        {
          DbSet.AddAsync(entity);
    
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = DbSet;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllListAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            return await query.Where(filter).ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
             
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void UpdateExisting(T entity)
        {
            DbSet.Update(entity);
        }
    }
}
