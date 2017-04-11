namespace JPGPizza.Data.Configurations
{
    using JPGPizza.Models;
    using System.Data.Entity.ModelConfiguration;

    public class OrderProductConfiguration : EntityTypeConfiguration<OrderProduct>
    {
        public OrderProductConfiguration()
        {
            // Set primary key.
            this.HasKey(od => new { od.OrderId, od.ProductId });

            // Set required fields.
            this.Property(od => od.Quantity).IsRequired();

            // Set relationships.
            this.HasRequired(od => od.Order)
                .WithMany(o => o.OrderProducts)
                .WillCascadeOnDelete(false);

            this.HasRequired(od => od.Product)
                .WithMany(p => p.OrderProducts)
                .WillCascadeOnDelete(false);
        }
    }
}
