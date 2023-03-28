using Microsoft.EntityFrameworkCore;
using PharmacyEdition.Domain.Entities;
using PharmacyEditon.Data.AppDbContext;
using PharmacyEditon.Data.IRepositories;

namespace PharmacyEditon.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PharmacyDbContext context = new PharmacyDbContext();
        public async Task<bool> DeleteAsync(Predicate<Order> predicate)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }

            var entityToDelete = this.context.Orders.ToList().Where(x => predicate(x)).ToList();

            if (entityToDelete is null)
            {
                return false;
            }

            context.Orders.RemoveRange(entityToDelete);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Order> InsertAsync(Order entity)
        {
            var insertedEntity = await context.Orders.AddAsync(entity);
            await context.SaveChangesAsync();

            return insertedEntity.Entity;
        }

        public List<Order> SelectAllAsync(Predicate<Order> predicate = null)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }
            return this.context.Orders.Where(x => predicate(x)).ToList();
        }

        public async Task<Order> SelectAsync(Predicate<Order> predicate = null)
        {
            if (predicate is null)
            {
                predicate = x => true;
            }
            return await context.Orders.FirstOrDefaultAsync(x => predicate(x));
        }

        public async Task<Order> UpdateAsync(long id, Order entity)
        {
            var updatedEntity = context.Orders.Update(entity);
            await context.SaveChangesAsync();

            return updatedEntity.Entity;
        }
    }
}
