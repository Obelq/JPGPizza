namespace JPGPizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageFieldToProductAndImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageUrl", c => c.String(nullable: false));
            DropColumn("dbo.Products", "Picture");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Picture", c => c.String(nullable: false));
            DropColumn("dbo.Products", "ImageUrl");
        }
    }
}
