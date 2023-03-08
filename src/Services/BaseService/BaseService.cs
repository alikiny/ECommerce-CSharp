namespace Backend.src.Services.BaseService
{
    public class BaseService<T, TReadDto, TCreateDto, TUpdateDto>
        : IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
        where T : BaseModel
    {
        protected readonly DatabaseContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<T> _dbSet;

        public BaseService(IMapper mapper, DatabaseContext context)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<List<TReadDto>> GetAllAsync(GetAllQueryOptions options)
        {
            var query = _dbSet.AsNoTracking().AsQueryable();
            // Sorting
            query = from p in query orderby $"{options.Order} {options.OrderBy}" select p;
            // Limit and offset
            query = query.Skip(options.Offset).Take(options.Limit);
            return _mapper.Map<List<T>, List<TReadDto>>(await query.ToListAsync());
        }

        public virtual async Task<TReadDto> GetByIdAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            return _mapper.Map<T, TReadDto>(entity);
        }

        public virtual async Task<TReadDto> AddOneAsync(TCreateDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<T, TReadDto>(entity);
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<TReadDto> UpdateOneAsync(int id, TUpdateDto update)
        {
            var entity = await FindByIdAsync(id);
            foreach (var property in update!.GetType().GetProperties())
            {
                if (
                    !string.IsNullOrEmpty((string?)property.GetValue(update))
                    || !string.IsNullOrEmpty((string?)property.GetValue(update))
                )
                {
                    entity
                        .GetType()
                        .GetProperty(property.Name)!
                        .SetValue(entity, property.GetValue(update));
                }
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<T, TReadDto>(entity);
        }

        private async Task<T> FindByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null)
            {
                throw ServiceException.NotFound(
                    $"{_context.Model.FindEntityType(typeof(T))} with id {id} is not found"
                );
            }
            else
            {
                return entity;
            }
        }
    }
}
