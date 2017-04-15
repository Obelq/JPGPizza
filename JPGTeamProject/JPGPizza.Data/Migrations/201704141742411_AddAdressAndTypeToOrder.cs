namespace JPGPizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdressAndTypeToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderProducts", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.OrderProducts", "Adress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderProducts", "Adress");
            DropColumn("dbo.OrderProducts", "Type");
        }
    }
}
