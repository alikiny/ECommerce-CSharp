namespace Backend.src.Services.BaseService
{
    public interface IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
    {
        Task<IEnumerable<TReadDto>> GetAllAsync(GetAllQueryOptions options);
        Task<TReadDto?> GetByIdAsync(int id);
        Task<TReadDto> AddOneAsync(TCreateDto dto);
        Task<bool> DeleteByIdAsync(int id);
        Task<TReadDto> UpdateOneAsync(int id, TUpdateDto update);
    }

    public class GetAllQueryOptions
    {
        public int Limit { get; set; } = 30;
        public int Offset { get; set; } = 0;
        public string Order { get; set; } = "id";
        public OrderBy OrderBy { get; set; }
        public string Search { get; set; } = string.Empty;
    }

    public enum OrderBy
    {
        ASC,
        DESC
    }
}