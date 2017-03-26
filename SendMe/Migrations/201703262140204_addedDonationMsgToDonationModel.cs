namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDonationMsgToDonationModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donations", "DonationMsg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Donations", "DonationMsg");
        }
    }
}
