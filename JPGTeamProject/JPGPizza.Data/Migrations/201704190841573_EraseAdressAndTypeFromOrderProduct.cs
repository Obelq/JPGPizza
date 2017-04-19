namespace JPGPizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EraseAdressAndTypeFromOrderProduct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderProducts", "Type");
            DropColumn("dbo.OrderProducts", "Adress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderProducts", "Adress", c => c.String());
            AddColumn("dbo.OrderProducts", "Type", c => c.Int(nullable: false));
        }
    }
}
