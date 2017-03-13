namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailVerification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "EmailAddress", c => c.String());
            AddColumn("dbo.AspNetUsers", "EmailVerficationToken", c => c.String());
            AddColumn("dbo.AspNetUsers", "IsEmailVerified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsEmailVerified");
            DropColumn("dbo.AspNetUsers", "EmailVerficationToken");
            DropColumn("dbo.AspNetUsers", "EmailAddress");
        }
    }
}
