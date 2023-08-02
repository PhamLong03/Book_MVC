using System.Linq.Expressions;

namespace BookWeb.Repository.IReporistory
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includes = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includes = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
    }
}
