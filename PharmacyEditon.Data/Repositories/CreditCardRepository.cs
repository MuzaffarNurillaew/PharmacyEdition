using Microsoft.EntityFrameworkCore;
using PharmacyEdition.Domain.Entities;
using PharmacyEditon.Data.AppDbContext;
using PharmacyEditon.Data.IRepositories;

namespace PharmacyEditon.Data.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly PharmacyDbContext context = new PharmacyDbContext();

        public async Task<bool> DeleteAsync(Predicate<CreditCard> predicate)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }

            var entityToDelete = this.context.CreditCards.ToList().Where(x => predicate(x)).ToList();

            if (entityToDelete is null)
            {
                return false;
            }

            context.CreditCards.RemoveRange(entityToDelete);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<CreditCard> InsertAsync(CreditCard entity)
        {
            var insertedEntity = await context.CreditCards.AddAsync(entity);
            await context.SaveChangesAsync();

            return insertedEntity.Entity;
        }

        public List<CreditCard> SelectAllAsync(Predicate<CreditCard> predicate = null)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }
            return this.context.CreditCards.ToList().Where(x => predicate(x)).ToList();
        }

        public CreditCard SelectAsync(Predicate<CreditCard> predicate = null)
        {
            if (predicate is null)
            {
                predicate = x => true;
            }
            return context.CreditCards.ToList().FirstOrDefault(x => predicate(x));
        }

        public async Task<CreditCard> UpdateAsync(long id, CreditCard entity)
        {
            var updatedEntity = context.CreditCards.Update(entity);
            await context.SaveChangesAsync();

            return updatedEntity.Entity;
        }
    }
}
