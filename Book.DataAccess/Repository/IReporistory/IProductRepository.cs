using Book.Models;

namespace BookWeb.Repository.IReporistory
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
