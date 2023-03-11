using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Repository.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseModel
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DatabaseContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddOneAsync(T data)
        {
            _dbSet.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteOneAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is not null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(GetAllQueryOptions options)
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(e => e.UpdatedAt).ToArrayAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateOneAsync(T data)
        {
            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return data;
        }
    }
}
