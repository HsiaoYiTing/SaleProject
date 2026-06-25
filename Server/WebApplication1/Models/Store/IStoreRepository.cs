public interface IStoreRepository
{
    Task<Store?> GetStoreByIdAsync(string id);
}