using HelloApi.Entities;

namespace HelloApi.Domain
{
    public interface ITPersonRepository
    {
        Task<IEnumerable<TPersonEntity>> GetAllAsync();
        Task<TPersonEntity?> GetByIdAsync(int id);
        Task<TPersonEntity> AddAsync(TPersonEntity person);
        Task<TPersonEntity?> UpdateAsync(TPersonEntity person);
        Task<bool> DeleteAsync(int id);
    }
}
