using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyBarberShop;
using MyBarberShop.Entities;

namespace MyBarberShop.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<CorteCabello> CorteCabello { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey("CategoryId");
                e.Property("CategoryId").ValueGeneratedOnAdd();
                e.HasData(
                    new Category { CategoryId = 1, Name = "Corte Clasico" },
                    new Category { CategoryId = 2, Name = "Corte Fade" },
                    new Category { CategoryId = 3, Name = "Corte Taper" }
                    );

            });

            modelBuilder.Entity<CorteCabello>(e =>
            {
                e.HasKey("CorteCabelloId");
                e.Property("CorteCabelloId").ValueGeneratedOnAdd();
                e.Property("Price").HasColumnType("decimal(10,2)");
                e.HasOne(e => e.Category).WithMany(c => c.CortesCabellos).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);

                 
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey("UserId");
                e.Property("UserId").ValueGeneratedOnAdd();

            });

        }
    }
}
