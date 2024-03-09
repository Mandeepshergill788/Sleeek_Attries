using RehmatCreations.Models;


namespace RehmatCreations.Repositories
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Category>> Categories();
        Task<IEnumerable<Product>> Getproducts(string sterm, int categoryId);
    }
}