namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCreatedToDonation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donations", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Donations", "Created");
        }
    }
}
