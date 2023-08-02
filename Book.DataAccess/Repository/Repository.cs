using Book.DataAccess.Data;
using BookWeb.Repository.IReporistory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            _db.Products.Include(u => u.Category).Include(u=>u.CategoryId);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public IEnumerable<T> GetAll(string? includes = null)
        {
            IQueryable<T> query = dbSet;
            if(!string.IsNullOrEmpty(includes))
            {
                foreach(var include in includes
                    .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }
            return query.ToList();
        }


        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includes = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includes))
            {
                foreach (var include in includes
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include);
                }
            }
            return query.Where(filter).FirstOrDefault();
        }
    }
}
