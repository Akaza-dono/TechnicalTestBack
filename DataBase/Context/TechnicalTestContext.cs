using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Context
{
    public class TechnicalTestContext : DbContext
    {
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Items> Items { get; set; } = null!;
        public DbSet<Sells> Sells { get; set; } = null!;
        public DbSet<Rols> Rols { get; set; } = null!;


        public TechnicalTestContext(DbContextOptions<TechnicalTestContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<Items>().ToTable("items");
            modelBuilder.Entity<Sells>().ToTable("sells");

            base.OnModelCreating(modelBuilder);
        }
    }
}
