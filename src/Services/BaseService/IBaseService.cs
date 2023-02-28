namespace Backend.src.Services.BaseService
{
    public interface IBaseService<T, TRead, TCreate, TUpdate>
    {
        Task<List<T>> GetAllAsync(string orderBy, int limit, int offset);
        Task<T> GetByIdAsync(int id);
        Task<T> AddOneAsync(TCreate dto);
        Task<bool> DeleteByIdAsync(int id);
        Task<T> UpdateOneAsync(int id, TUpdate update);
    }
}