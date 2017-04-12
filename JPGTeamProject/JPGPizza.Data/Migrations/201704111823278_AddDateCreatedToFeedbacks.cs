namespace JPGPizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateCreatedToFeedbacks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "RegisteredOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RegisteredOn");
            DropColumn("dbo.Feedbacks", "CreatedOn");
        }
    }
}
