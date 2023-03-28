using Microsoft.EntityFrameworkCore;
using PharmacyEdition.Domain.Entities;
using PharmacyEdition.Models;

namespace PharmacyEditon.Data.AppDbContext
{
    public class PharmacyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=PharmacyEdition;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>(ot => ot.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.CreditCard)
                .WithOne(c => c.Payment)
                .HasForeignKey<Payment>(p => p.CreditCardId);
        }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}