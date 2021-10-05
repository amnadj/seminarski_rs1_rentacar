using AjAM_seminarski.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AjAM_seminarski.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Feature_Car_Details> Feature_Car_Details { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Features_Details> Features_Details { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist>Wishlists { get; set; }
        public DbSet<CarComments> CarComments { get; set; }


        internal object Find(int carId)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() }, new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Family" });
            modelBuilder.Entity<Car>().HasData(new Car { Id = 1, Name = "Auto 1", Amount = 5, Description="Super brzo auto", CategoryID = 1, Price = 120 });
            modelBuilder.Entity<Car>().HasData(new Car { Id = 2, Name = "Auto 2", Amount = 15, Description = "Super brzo auto", CategoryID = 1, Price = 120 });
            modelBuilder.Entity<Car>().HasData(new Car { Id = 3, Name = "Auto 3", Amount = 3, Description = "Super brzo auto", CategoryID = 1, Price = 160 });
            modelBuilder.Entity<Car>().HasData(new Car { Id = 4, Name = "Auto 4", Amount = 2, Description = "Super brzo auto", CategoryID = 1, Price = 110 });
            modelBuilder.Entity<Car>().HasData(new Car { Id = 5, Name = "Auto 5", Amount = 7, Description = "Super brzo auto", CategoryID = 1, Price = 70 });


        }
    }
}
