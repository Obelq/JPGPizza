namespace JPGPizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeIingredientsNamePropertyUqinue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingredients", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Ingredients", "Name", unique: true, name: "IX_Ingredients_Name");
            DropColumn("dbo.Products", "Description");
            DropColumn("dbo.Ingredients", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredients", "Description", c => c.String());
            AddColumn("dbo.Products", "Description", c => c.String(maxLength: 255));
            DropIndex("dbo.Ingredients", "IX_Ingredients_Name");
            AlterColumn("dbo.Ingredients", "Name", c => c.String());
        }
    }
}
