namespace Backend.src.Services.BaseService
{
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
    where TEntity : class
    where TDto : class
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private DbSet<TEntity> _dbSet { get; }

        public BaseService(IMapper mapper, DatabaseContext context)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(
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

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) throw ServiceException.NotFound($"Product with id {id} is not found");
            return entity;
        }

        public virtual async Task<TEntity> AddOneAsync(TDto dto)
        {
            throw new Exception("test error in base service");
            var entity = _mapper.Map<TEntity>(dto);
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

        public virtual async Task<TEntity> UpdateOneAsync(int id, TDto update)
        {
            var entity = await GetByIdAsync(id);
            _mapper.Map(update, entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}