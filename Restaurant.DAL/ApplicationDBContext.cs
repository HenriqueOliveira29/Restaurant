using Microsoft.EntityFrameworkCore;
using Restaurant.Infraestructure.Entities;
using Restaurante.Models;

namespace Restaurant.DAL
{
    public class ApplicationDBContext : AuditableIdentityContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ingredients>(options =>
            {
                options.HasKey(a => a.Id);

                options.HasOne(a => a.Stock).WithOne(a => a.Ingredient).HasForeignKey<Stock>(a => a.IngredientId);
            });
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemIngredients> ItemIngredients { get; set; }
        public virtual DbSet<ItemRequest> ItemRequests { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Restaurante.Models.Restaurant> Restaurants { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Waitress> Waitresses { get; set; }


    }
}
