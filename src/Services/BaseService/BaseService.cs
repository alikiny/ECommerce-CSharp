using Backend.src.Helpers;

namespace Backend.src.Services.BaseService
{
    public class BaseService<T, TReadDto, TCreateDto, TUpdateDto>
        : IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
        where T : BaseModel
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IMapper mapper, IBaseRepository<T> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TReadDto>> GetAllAsync(GetAllQueryOptions options)
        {
            var data = await _repository.GetAllAsync(options);
            return _mapper.Map<IEnumerable<T>, IEnumerable<TReadDto>>(data);
        }

        public virtual async Task<TReadDto?> GetByIdAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            return _mapper.Map<T, TReadDto>(entity);
        }

        public virtual async Task<TReadDto> AddOneAsync(TCreateDto dto)
        {
            var data = _mapper.Map<T>(dto);
            var entity = await _repository.AddOneAsync(data);
            return _mapper.Map<T, TReadDto>(entity);
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteOneAsync(id);
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

            entity = await _repository.UpdateOneAsync(entity);
            return _mapper.Map<T, TReadDto>(entity);
        }

        private async Task<T> FindByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
            {
                throw ServiceException.NotFound($"{(typeof(T))} with id {id} is not found");
            }
            else
            {
                return entity;
            }
        }
    }
}
