using Microsoft.EntityFrameworkCore;
using PharmacyEdition.Domain.Entities;
using PharmacyEditon.Data.AppDbContext;
using PharmacyEditon.Data.IRepositories;

namespace PharmacyEditon.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PharmacyDbContext context = new PharmacyDbContext();
        public async Task<bool> DeleteAsync(Predicate<Payment> predicate)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }

            var entityToDelete = this.context.Payments.ToList().Where(x => predicate(x)).ToList();

            if (entityToDelete is null)
            {
                return false;
            }

            context.Payments.RemoveRange(entityToDelete);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Payment> InsertAsync(Payment entity)
        {
            var insertedEntity = await context.Payments.AddAsync(entity);
            await context.SaveChangesAsync();

            return insertedEntity.Entity;
        }

        public List<Payment> SelectAllAsync(Predicate<Payment> predicate = null)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }
            return this.context.Payments.ToList().Where(x => predicate(x)).ToList();
        }

        public Payment SelectAsync(Predicate<Payment> predicate = null)
        {
            if (predicate is null)
            {
                predicate = x => true;
            }
            return context.Payments.ToList().FirstOrDefault(x => predicate(x));
        }

        public async Task<Payment> UpdateAsync(long id, Payment entity)
        {
            var updatedEntity = context.Payments.Update(entity);
            await context.SaveChangesAsync();

            return updatedEntity.Entity;
        }
    }
}
