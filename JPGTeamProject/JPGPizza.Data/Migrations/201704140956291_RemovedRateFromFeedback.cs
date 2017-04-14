namespace JPGPizza.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRateFromFeedback : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Feedbacks", "Rate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "Rate", c => c.Int(nullable: false));
        }
    }
}
