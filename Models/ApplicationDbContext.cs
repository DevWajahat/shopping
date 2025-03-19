using Microsoft.EntityFrameworkCore;

namespace shoppingCart.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
         
          
        }

        public DbSet<Dealer> tbl_Dealer { get; set; }
        public DbSet<Customer> tbl_Customer { get; set; }
        public DbSet<Category> tbl_Category { get; set; }
        public DbSet<Product> tbl_Product { get; set; }
        public DbSet<Cart> tbl_Cart { get; set; }
        public DbSet<Feedback> tbl_feedback { get; set; }
        public DbSet<FAQs> tbl_FAQs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasOne(p => p.category)
                 .WithMany(c => c.Product)
                 .HasForeignKey(p => p.cat_id);
        }
    }
}
