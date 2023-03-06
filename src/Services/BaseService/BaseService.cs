namespace Backend.src.Services.BaseService
{
    public class BaseService<T, TReadDto, TCreateDto, TUpdateDto> : IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
    where T : BaseModel
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

        public virtual async Task<List<TReadDto>> GetAllAsync(GetAllQueryOptions options)
        {
            var query = _dbSet.AsQueryable();
            // Sorting
            query = from p in query orderby $"{options.Order} {options.OrderBy}" select p;
            // Limit and offset
            query = query.Skip(options.Offset).Take(options.Limit);

            return _mapper.Map<List<T>, List<TReadDto>>(await query.ToListAsync());
        }

        public virtual async Task<TReadDto> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) throw ServiceException.NotFound($"{_context.Model.FindEntityType(typeof(T))} with id {id} is not found");
            return _mapper.Map<T, TReadDto>(entity);
        }

        public virtual async Task<TReadDto> AddOneAsync([FromBody] TCreateDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<T, TReadDto>(entity);
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _dbSet.Remove(_mapper.Map<TReadDto, T>(entity));
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<TReadDto> UpdateOneAsync(int id, TUpdateDto update)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw ServiceException.NotFound(
                $"{_context.Model.FindEntityType(typeof(T))} with id {id} is not found"
                               );
            }
            else
            {
                foreach (var property in update.GetType().GetProperties())
                {
                    if (!string.IsNullOrEmpty((string?)property.GetValue(update)) || !string.IsNullOrEmpty((string?)property.GetValue(update)))
                    {
                        entity.GetType().GetProperty(property.Name)!.SetValue(entity, property.GetValue(update));
                    }
                }
                await _context.SaveChangesAsync();

                return _mapper.Map<T, TReadDto>(entity);
            }
        }
    }
}