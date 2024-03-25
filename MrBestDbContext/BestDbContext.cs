using Microsoft.EntityFrameworkCore;
using MrBestPizza.Entities;
using System;

namespace MrBestPizza.MrBestDbContext
{
    public class BestDbContext : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public BestDbContext(DbContextOptions<BestDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().
                HasData(
               new Category("Calzone")
               {
                   Id = 1,
                   Description = "Pizza folded in half turnover-style."
               },
               new Category("Pizzetta")
               {
                   Id = 2,
                   
                   Description = "Small pizza served as snack."
               },
               new Category("Deep-fried pizza ")
               {
                   Id = 3,
                 
                   Description = "The pizza is deep-fried (cooked in oil) instead of baked."
               },
               new Category("California-style pizza")
               {
                   Id = 4,


                   Description = "Distinguished by the use of non-traditional ingredients," +
                   " especially varieties of fresh produce."

               }); 
            modelBuilder.Entity<Pizza>().
                HasData(
                new Pizza()
                {
                    PizzaId = 101,
                    Name = "Chicken Pizza",
                    Description = "A Pizza that enriches the soul",
                    Price = 29m,
                    CategoryId = 1
                    },
                 new Pizza()
                 {
                     PizzaId =122 ,
                     Name = "Beef",
                     Description = "A Pocket friendly Pizza",
                     Price = 29.7m,
                     CategoryId =2
                 },
                  new Pizza()
                  {
                      PizzaId =1123 ,
                      Name = "Seasoned",
                      Description = "A blend of nutrient",
                      Price = 23.8m,
                      CategoryId =4
                      },
                   new Pizza()
                   {
                       PizzaId = 127,
                       Name = "Spicy Pizza",
                       Description = "Specially made for you",
                       Price = 50m,
                       CategoryId =4
                   },
                    new Pizza()
                    {
                        PizzaId = 324,
                        Name = "Vegie Pizza",
                        Description = "A blend of vegetables",
                        Price = 46.5m,
                        CategoryId =3
                        },
                    new Pizza()
                    {
                        PizzaId =453,
                        Name = "French Pizza",
                        Description = "Pizza de French",
                        Price = 45.6m,
                        CategoryId =2
                        },
                    new Pizza()
                    {
                        PizzaId = 1009,
                        Name = "Plain ",
                        Description = "No beef Just plain",
                        Price = 34.8m,
                        CategoryId =1
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
