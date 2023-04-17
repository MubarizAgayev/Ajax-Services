using EntityFramework_Slider.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework_Slider.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDelete);


            modelBuilder.Entity<Setting>()
            .HasData(
                new Setting
                {
                    Id = 1,
                    Key = "HeaderLogo",
                    Value = "logo.png"
                },
                new Setting
                {
                    Id = 2,
                    Key = "Phone",
                    Value = "32525352525"
                },
                new Setting
                {
                    Id = 4,
                    Key = "Email",
                    Value = "P135@code.edu.az"
                }
            );
        }




    }
}
