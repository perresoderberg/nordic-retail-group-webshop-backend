using Microsoft.EntityFrameworkCore;
using nordicretailgroup.webshop.Domain;

namespace nordicretailgroup.webshop.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id).HasColumnName("id");
            entity.Property(x => x.Name).HasColumnName("name").IsRequired();
            entity.Property(x => x.Slug).HasColumnName("slug").IsRequired();
            entity.Property(x => x.Image).HasColumnName("image");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id).HasColumnName("id");
            entity.Property(x => x.Title).HasColumnName("title").IsRequired();
            entity.Property(x => x.Description).HasColumnName("description").IsRequired();
            entity.Property(x => x.Price).HasColumnName("price").HasPrecision(12, 2);
            entity.Property(x => x.DiscountPercentage).HasColumnName("discount_percentage").HasPrecision(8, 2);
            entity.Property(x => x.Rating).HasColumnName("rating").HasPrecision(4, 2);
            entity.Property(x => x.Stock).HasColumnName("stock");
            entity.Property(x => x.Brand).HasColumnName("brand");
            entity.Property(x => x.Sku).HasColumnName("sku").IsRequired();
            entity.Property(x => x.Weight).HasColumnName("weight").HasPrecision(10, 2);
            entity.Property(x => x.WarrantyInformation).HasColumnName("warranty_information");
            entity.Property(x => x.ShippingInformation).HasColumnName("shipping_information");
            entity.Property(x => x.AvailabilityStatus).HasColumnName("availability_status");
            entity.Property(x => x.ReturnPolicy).HasColumnName("return_policy");
            entity.Property(x => x.MinimumOrderQuantity).HasColumnName("minimum_order_quantity");
            entity.Property(x => x.Thumbnail).HasColumnName("thumbnail");
            entity.Property(x => x.CategoryId).HasColumnName("category_id");

            entity.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
