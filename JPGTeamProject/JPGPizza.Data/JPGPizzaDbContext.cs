using JPGPizza.Data.Migrations;

namespace JPGPizza.Data
{
    using JPGPizza.Data.Configurations;
    using JPGPizza.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class JPGPizzaDbContext : IdentityDbContext<ApplicationUser>
    {
        public JPGPizzaDbContext()
            : base("name=JPGPizzaDbContext")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<JPGPizzaDbContext, Configuration>());
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Ingredient> Ingredinets { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new OrderProductConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new IngredientConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static JPGPizzaDbContext Create()
        {
            return new JPGPizzaDbContext();
        }
    }
}