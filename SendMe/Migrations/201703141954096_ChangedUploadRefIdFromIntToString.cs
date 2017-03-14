namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUploadRefIdFromIntToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Uploads", "RefId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Uploads", "RefId", c => c.Int());
        }
    }
}
