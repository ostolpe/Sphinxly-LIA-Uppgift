using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebAPI.Data.Contexts;
using WebAPI.Data.Entities;

namespace WebAPI.Data.Repositories
{
    public class ProductRepository(DataContext context) : IProductRepository
    {
        private readonly DataContext _context = context;

        public async Task<bool> AddAsync(ProductEntity product)
        {
            if (product == null)
                return false;

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ProductEntity>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductEntity?> GetAsync(int id)
        {
            var result = await _context.Products.FindAsync(id);

            if (result == null)
                return null;

            return result;
        }

        public async Task<bool> UpdateAsync(ProductEntity product)
        {
            if (product == null)
                return false;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return false;

            _context.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> expression)
        {
            var result = await _context.Products.AnyAsync(expression);

            return result;
        }
    }
}
