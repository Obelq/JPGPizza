namespace JPGPizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pleasework : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            AddForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderProducts", "Adress", c => c.String());
            AddColumn("dbo.OrderProducts", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            AddForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders", "Id");
        }
    }
}
