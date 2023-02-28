namespace Backend.src.Services.BaseService
{
    public class BaseService<T, TRead, TCreate, TUpdate> : IBaseService<T, TRead, TCreate, TUpdate>
    where T : class
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private DbSet<T> _dbSet { get; }

        public BaseService(IMapper mapper, DatabaseContext context)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync(
            string orderBy, //For ex: "name asc"
            int limit,
            int offset
            )
        {
            var query = _dbSet.AsQueryable();
            // Sorting
            var property = orderBy.Split(" ")[0];
            var order = orderBy.Split(" ")[1];
            query = from p in query orderby $"{property} {order}" select p;
            // Limit and offset
            query = query.Skip(offset).Take(limit);

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) throw ServiceException.NotFound($"Product with id {id} is not found");
            return entity;
        }

        public virtual async Task<T> AddOneAsync([FromBody] TCreate dto)
        {
            var entity = _mapper.Map<T>(dto);
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<T> UpdateOneAsync(int id, [FromBody] TUpdate update)
        {
            var entity = await GetByIdAsync(id);
            foreach (var property in update.GetType().GetProperties())
            {
                if (property.GetValue(update) != null)
                {
                    entity.GetType().GetProperty(property.Name)!.SetValue(entity, property.GetValue(update));
                }
                Console.WriteLine(property.GetValue(update));
            }
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}