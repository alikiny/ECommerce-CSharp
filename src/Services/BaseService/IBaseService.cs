namespace Backend.src.Services.BaseService
{
    public interface IBaseService<T, TRead, TCreate, TUpdate>
    {
        Task<List<TRead>> GetAllAsync(string orderBy, int limit, int offset);
        Task<TRead> GetByIdAsync(int id);
        Task<TRead> AddOneAsync(TCreate dto);
        Task<bool> DeleteByIdAsync(int id);
        Task<TRead> UpdateOneAsync(int id, TUpdate? update);
    }
}