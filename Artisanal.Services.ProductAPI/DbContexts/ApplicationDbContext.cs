using Artisanal.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Artisanal.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
                
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                ProductName = "ChaiseBoisMassif",
                Price = 15,
                CategoryName = "categorie1",
                ImageURL = "1.jpg"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                ProductName = "RangementBoisMassif",
                Price = 15,
                CategoryName = "categorie2",
                ImageURL = "2.jpg"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                ProductName = "DecorArtis",
                Price = 15,
                CategoryName = "categorie1",
                ImageURL = "3.jpg"
            });
            
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 1,
                CategoryName = "categorie1"
            });
        }

    }
}