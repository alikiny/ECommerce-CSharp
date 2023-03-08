using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private DatabaseContext _context {get;}
        private DbSet<Product> _dbSet {get;}
        public ProductRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Products;
        }
        public async Task<Product?> GetByIdAsync (int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
