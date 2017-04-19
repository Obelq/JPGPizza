namespace JPGPizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedOrdersRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerOrders", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerOrders", "OrderId", "dbo.Orders");
            DropIndex("dbo.CustomerOrders", new[] { "CustomerId" });
            DropIndex("dbo.CustomerOrders", new[] { "OrderId" });
            AddColumn("dbo.Orders", "CustomerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropTable("dbo.CustomerOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomerOrders",
                c => new
                    {
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.OrderId });
            
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropColumn("dbo.Orders", "CustomerId");
            CreateIndex("dbo.CustomerOrders", "OrderId");
            CreateIndex("dbo.CustomerOrders", "CustomerId");
            AddForeignKey("dbo.CustomerOrders", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerOrders", "CustomerId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
