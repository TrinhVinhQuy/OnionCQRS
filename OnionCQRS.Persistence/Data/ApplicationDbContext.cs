using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionCQRS.Domain.Entities;
using System.Reflection.Emit;

namespace OnionCQRS.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration, IServiceProvider serviceProvider)
              : base(options)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            // Seed data for Product
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
            // Seed data for IdentityRole
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );
            // Seed data for ApplicationUser
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "1",
                    Address = "Việt Nam",
                    Fullname = "Admin",
                    UserName = "user1@example.com",
                    NormalizedUserName = "USER1@EXAMPLE.COM",
                    Email = "user1@example.com",
                    NormalizedEmail = "USER1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEOwYP0jYrs5jNzwpDH0zrFMtPJ6WbPdpAq2f5fs73OTB2v2Y28M9OEm9TfDeIydmQA==", // Password is "password"
                    SecurityStamp = "SOMESECURITYSTAMP",
                    ConcurrencyStamp = "SOMECONCURRENCYSTAMP"
                }
            );
            // Seed data for UserRole
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "1", RoleId = "1" } // user1@example.com is Admin
            );
        }
    }
}
