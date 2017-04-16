namespace JPGPizza.Data.Configurations
{
    using JPGPizza.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    class IngredientConfiguration : EntityTypeConfiguration<Ingredient>
    {
        public IngredientConfiguration()
        {
            // Set length
            this.Property(p => p.Name).HasMaxLength(50);

            // Set required fiedls.
            this.Property(p => p.Name).IsRequired();

            // Set indeces
            this.Property(p => p.Name)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                   new IndexAnnotation(
                       new IndexAttribute("IX_Ingredients_Name", 1) { IsUnique = true }));
        }
    }
}
