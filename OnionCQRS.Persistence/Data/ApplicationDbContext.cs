using Microsoft.EntityFrameworkCore;
using OnionCQRS.Domain.Entities;

namespace OnionCQRS.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
              : base(options)
        { }
        public DbSet<Product> Product { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "IPhone 13",
                    Barcode = "iphone-13-series",
                    Description = "Là dòng điện thoại nhỏ gọn",
                    Rate = 10,
                },
                new Product
                {
                    Id = 2,
                    Name = "IPhone 14 Promax",
                    Barcode = "iphone-14-series",
                    Description = "Là dòng điện thoại sang trọng",
                    Rate = 12,
                }
            );
        }
    }
}
