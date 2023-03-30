using Microsoft.EntityFrameworkCore;
using PharmacyEdition.Domain.Entities;
using PharmacyEditon.Data.AppDbContext;
using PharmacyEditon.Data.IRepositories;

namespace PharmacyEditon.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PharmacyDbContext context = new PharmacyDbContext();
        public async Task<bool> DeleteAsync(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }

            var entityToDelete = this.context.Users.ToList().Where(x => predicate(x));

            if (entityToDelete is null)
            {
                return false;
            }

            context.Users.RemoveRange(entityToDelete);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<User> InsertAsync(User entity)
        {
            var insertedEntity = await context.Users.AddAsync(entity);
            await context.SaveChangesAsync();

            return insertedEntity.Entity;
        }

        public List<User> SelectAllAsync(Predicate<User> predicate = null)
        {
            if (predicate == null)
            {
                predicate = x => true;
            }
            return this.context.Users.ToList().Where(x => predicate(x)).ToList();
        }

        public User SelectAsync(Predicate<User> predicate = null)
        {
            if (predicate is null)
            {
                predicate = x => true;
            }
            return context.Users.ToList().FirstOrDefault(x => predicate(x));
        }

        public async Task<User> UpdateAsync(long id, User entity)
        {
            var updatedEntity = context.Users.Update(entity);
            await context.SaveChangesAsync();

            return updatedEntity.Entity;
        }
    }
}