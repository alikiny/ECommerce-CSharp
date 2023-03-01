namespace Backend.src.Services.BaseService
{
    public interface IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
    {
        Task<List<TReadDto>> GetAllAsync(string orderBy, int limit, int offset);
        Task<TReadDto> GetByIdAsync(int id);
        Task<TReadDto> AddOneAsync(TCreateDto dto);
        Task<bool> DeleteByIdAsync(int id);
        Task<TReadDto> UpdateOneAsync(int id, TUpdateDto? update);
    }
}