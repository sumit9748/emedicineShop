using Emedicine.DAL.model;
using Microsoft.EntityFrameworkCore;


namespace Emedicine.DAL.Data
{
    public class MedicineDbContext:DbContext
    {
        public MedicineDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Medicalshop> Medicalshops { get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<MedicalShopItem> MedicalShopItems { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set;}


    }
}
