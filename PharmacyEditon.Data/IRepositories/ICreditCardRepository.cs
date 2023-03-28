using PharmacyEdition.Domain.Entities;

namespace PharmacyEditon.Data.IRepositories
{
    public interface ICreditCardRepository
    {
        Task<CreditCard> InsertAsync(CreditCard entity);
        Task<CreditCard> UpdateAsync(long id, CreditCard entity);
        Task<bool> DeleteAsync(Predicate<CreditCard> predicate);
        CreditCard SelectAsync(Predicate<CreditCard> predicate = null);
        List<CreditCard> SelectAllAsync(Predicate<CreditCard> predicate = null);
    }
}
