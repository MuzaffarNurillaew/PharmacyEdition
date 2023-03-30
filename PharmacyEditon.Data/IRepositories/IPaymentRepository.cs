using PharmacyEdition.Domain.Entities;

namespace PharmacyEditon.Data.IRepositories
{
    public interface IPaymentRepository
    {
        Task<Payment> InsertAsync(Payment entity);
        Task<Payment> UpdateAsync(long id, Payment entity);
        Task<bool> DeleteAsync(Predicate<Payment> predicate);
        Payment SelectAsync(Predicate<Payment> predicate = null);
        List<Payment> SelectAllAsync(Predicate<Payment> predicate = null);
    }
}
