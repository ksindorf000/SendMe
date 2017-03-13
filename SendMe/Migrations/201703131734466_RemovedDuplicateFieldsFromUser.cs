namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDuplicateFieldsFromUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "EmailAddress");
            DropColumn("dbo.AspNetUsers", "EmailVerficationToken");
            DropColumn("dbo.AspNetUsers", "IsEmailVerified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsEmailVerified", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "EmailVerficationToken", c => c.String());
            AddColumn("dbo.AspNetUsers", "EmailAddress", c => c.String());
        }
    }
}
