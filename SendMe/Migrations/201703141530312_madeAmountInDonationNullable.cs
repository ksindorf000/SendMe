namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeAmountInDonationNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Donations", "Amount", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Donations", "Amount", c => c.Double(nullable: false));
        }
    }
}
