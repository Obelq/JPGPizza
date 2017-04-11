namespace JPGPizza.Data.Configurations
{
    using JPGPizza.Models;
    using System.Data.Entity.ModelConfiguration;

    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            // Set required fields.
            this.Property(p => p.FirstName).IsRequired();
            this.Property(p => p.LastName).IsRequired();

            // Set fields length.
            this.Property(p => p.FirstName).HasMaxLength(25);
            this.Property(p => p.LastName).HasMaxLength(25);

            // Set optional fieds.
            this.Property(p => p.Age).IsOptional();
            this.Property(p => p.Gender).IsOptional();

            // Set relationships.
            this.HasMany(c => c.Feedbacks).WithRequired(f => f.Customer).WillCascadeOnDelete(true);
            this.HasMany(c => c.Orders)
                .WithMany(o => o.Customers)
                .Map(co => 
                {
                    co.MapLeftKey("CustomerId");
                    co.MapRightKey("OrderId");
                    co.ToTable("CustomerOrders");
                });
        }
    }
}
