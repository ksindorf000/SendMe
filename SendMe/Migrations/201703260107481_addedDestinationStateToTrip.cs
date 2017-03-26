namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDestinationStateToTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "DestinationState", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "DestinationState");
        }
    }
}
