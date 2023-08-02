using Book.Models;

namespace BookWeb.Repository.IReporistory
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
