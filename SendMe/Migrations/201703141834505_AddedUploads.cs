namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUploads : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Uploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        File = c.String(),
                        TypeRef = c.String(),
                        RefId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Uploads");
        }
    }
}
