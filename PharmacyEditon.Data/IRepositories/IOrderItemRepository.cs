using PharmacyEdition.Domain.Entities;

namespace PharmacyEditon.Data.IRepositories
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> InsertAsync(OrderItem entity);
        Task<OrderItem> UpdateAsync(long id, OrderItem entity);
        Task<bool> DeleteAsync(Predicate<OrderItem> predicate);
        Task<OrderItem> SelectAsync(Predicate<OrderItem> predicate = null);
        List<OrderItem> SelectAllAsync(Predicate<OrderItem> predicate = null);
    }
}
