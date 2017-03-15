namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeStuIdinTripModelNullableTEMP : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "StuId", "dbo.StuProfiles");
            DropIndex("dbo.Trips", new[] { "StuId" });
            AlterColumn("dbo.Trips", "StuId", c => c.Int());
            CreateIndex("dbo.Trips", "StuId");
            AddForeignKey("dbo.Trips", "StuId", "dbo.StuProfiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "StuId", "dbo.StuProfiles");
            DropIndex("dbo.Trips", new[] { "StuId" });
            AlterColumn("dbo.Trips", "StuId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trips", "StuId");
            AddForeignKey("dbo.Trips", "StuId", "dbo.StuProfiles", "Id", cascadeDelete: true);
        }
    }
}
