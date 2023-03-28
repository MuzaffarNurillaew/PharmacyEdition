using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Models;

namespace PharmacyEditon.Data.IRepositories
{
    public interface IMedicineRepository
    {
        Task<Medicine> InsertAsync(Medicine entity);
        Task<Medicine> UpdateAsync(long id, Medicine entity);
        Task<bool> DeleteAsync(Predicate<Medicine> predicate);
        Task<Medicine> SelectAsync(Predicate<Medicine> predicate = null);
        List<Medicine> SelectAllAsync(Predicate<Medicine> predicate = null);
    }
}
