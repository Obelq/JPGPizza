namespace JPGPizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTypeFromOrderProduct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderProducts", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderProducts", "Type", c => c.Int(nullable: false));
        }
    }
}
