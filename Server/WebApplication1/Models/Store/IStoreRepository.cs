public interface IStoreRepository
{
    Task<List<Store>> GetAllAsync();
    Task<Store?> GetStoreByIdAsync(string id);
}