namespace Backend.src.Services.BaseService
{
    public interface IBaseService<T, K>
    {
        Task<List<T>> GetAllAsync(string orderBy, int limit, int offset);
        Task<T> GetByIdAsync(int id);
        Task<T> AddOneAsync(K dto);
        Task<bool> DeleteByIdAsync(int id);
        Task<T> UpdateOneAsync(int id, K update);
    }
}