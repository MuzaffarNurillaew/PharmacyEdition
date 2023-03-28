using Microsoft.EntityFrameworkCore;
using PharmacyEdition.Domain.Entities;
using PharmacyEditon.Data.AppDbContext;
using PharmacyEditon.Data.IRepositories;

namespace PharmacyEditon.Data.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly PharmacyDbContext context = new PharmacyDbContext();
        public async Task<bool> DeleteAsync(Predicate<OrderItem> predicate)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }

            var entityToDelete = this.context.OrderItems.Where(x => predicate(x)).ToList();

            if (entityToDelete is null)
            {
                return false;
            }

            context.OrderItems.RemoveRange(entityToDelete);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderItem> InsertAsync(OrderItem entity)
        {
            var insertedEntity = await context.OrderItems.AddAsync(entity);
            await context.SaveChangesAsync();

            return insertedEntity.Entity;
        }

        public List<OrderItem> SelectAllAsync(Predicate<OrderItem> predicate = null)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }
            return this.context.OrderItems.Where(x => predicate(x)).ToList();
        }

        public async Task<OrderItem> SelectAsync(Predicate<OrderItem> predicate = null)
        {
            if (predicate is null)
            {
                predicate = x => true;
            }
            return await context.OrderItems.FirstOrDefaultAsync(x => predicate(x));
        }

        public async Task<OrderItem> UpdateAsync(long id, OrderItem entity)
        {
            var updatedEntity = context.OrderItems.Update(entity);
            await context.SaveChangesAsync();

            return updatedEntity.Entity;
        }
    }
}
