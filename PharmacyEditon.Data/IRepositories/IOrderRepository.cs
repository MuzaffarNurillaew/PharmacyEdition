using PharmacyEdition.Domain.Entities;

namespace PharmacyEditon.Data.IRepositories
{
    public interface IOrderRepository
    {
        Task<Order> InsertAsync(Order entity);
        Task<Order> UpdateAsync(long id, Order entity);
        Task<bool> DeleteAsync(Predicate<Order> predicate);
        Task<Order> SelectAsync(Predicate<Order> predicate = null);
        List<Order> SelectAllAsync(Predicate<Order> predicate = null);
    }
}
