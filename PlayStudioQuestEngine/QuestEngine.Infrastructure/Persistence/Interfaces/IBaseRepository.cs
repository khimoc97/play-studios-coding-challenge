namespace QuestEngine.Infrastructure.Persistence.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> GetByIdAsync(string id);
        Task<List<T>> GetAllAsync();
        Task<T> UpdateByIdAsync(string id, T entity);

        Task<bool> CheckSeedingDataExist();
    }
}
