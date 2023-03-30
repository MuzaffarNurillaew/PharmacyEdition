using Microsoft.EntityFrameworkCore;
using PharmacyEdition.Models;
using PharmacyEditon.Data.AppDbContext;
using PharmacyEditon.Data.IRepositories;

namespace PharmacyEditon.Data.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly PharmacyDbContext context = new PharmacyDbContext();
        public async Task<bool> DeleteAsync(Predicate<Medicine> predicate)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }

            var entityToDelete = this.context.Medicines.ToList().Where(x => predicate(x)).ToList();

            if (entityToDelete is null)
            {
                return false;
            }

            context.Medicines.RemoveRange(entityToDelete);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Medicine> InsertAsync(Medicine entity)
        {
            var insertedEntity = await context.Medicines.AddAsync(entity);
            await context.SaveChangesAsync();

            return insertedEntity.Entity;
        }

        public List<Medicine> SelectAllAsync(Predicate<Medicine> predicate = null)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }
            return this.context.Medicines.ToList().Where(x => predicate(x)).ToList();
        }

        public Medicine SelectAsync(Predicate<Medicine> predicate = null)
        {
            if (predicate is null)
            {
                predicate = x => true;
            }
            return context.Medicines.ToList().FirstOrDefault(x => predicate(x));
        }

        public async Task<Medicine> UpdateAsync(long id, Medicine entity)
        {
            var updatedEntity = context.Medicines.Update(entity);
            await context.SaveChangesAsync();

            return updatedEntity.Entity;
        }
    }
}
