using System.Linq.Expressions;
using WebAPI.Data.Entities;

namespace WebAPI.Data.Repositories
{
    public interface IProductRepository
    {
        Task<bool> AddAsync(ProductEntity product);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> expression);
        Task<List<ProductEntity>> GetAllAsync();
        Task<ProductEntity?> GetAsync(int id);
        Task<bool> UpdateAsync(ProductEntity product);
    }
}