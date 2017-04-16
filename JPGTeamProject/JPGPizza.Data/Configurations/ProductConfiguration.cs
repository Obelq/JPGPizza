namespace JPGPizza.Data.Configurations
{
    using JPGPizza.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            // Set fields length.
            this.Property(p => p.Name).HasMaxLength(50);

            // Set required fields.
            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.Type).IsRequired();
            this.Property(p => p.ImageUrl).IsRequired();

            // Set indeces.
            this.Property(p => p.Name)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Products_Name", 1) { IsUnique = true }));

            // Set relationships.
            this.HasMany(p => p.Ingredients)
                .WithMany(i => i.Products)
                .Map(pi =>
                {
                    pi.MapLeftKey("ProductId");
                    pi.MapRightKey("IngredientId");
                    pi.ToTable("ProductIngredients");
                });

            this.HasMany(p => p.Feedbacks)
                .WithRequired(f => f.Product)
                .WillCascadeOnDelete(true);
        }
    }
}
