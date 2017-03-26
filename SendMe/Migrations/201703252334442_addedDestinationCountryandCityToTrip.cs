namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDestinationCountryandCityToTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "DestinationCountry", c => c.String());
            AddColumn("dbo.Trips", "DestinationCity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "DestinationCity");
            DropColumn("dbo.Trips", "DestinationCountry");
        }
    }
}
