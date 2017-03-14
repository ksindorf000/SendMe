namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTripTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Desc = c.String(),
                        Destination = c.String(),
                        DestLongLat = c.String(),
                        Dates = c.String(),
                        Deadline = c.DateTime(nullable: false),
                        TargetAmnt = c.Double(nullable: false),
                        PercentOfAmnt = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        StuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StuProfiles", t => t.StuId, cascadeDelete: true)
                .Index(t => t.StuId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "StuId", "dbo.StuProfiles");
            DropIndex("dbo.Trips", new[] { "StuId" });
            DropTable("dbo.Trips");
        }
    }
}
