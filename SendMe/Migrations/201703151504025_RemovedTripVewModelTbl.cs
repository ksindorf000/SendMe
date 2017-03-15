namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTripVewModelTbl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Donations", "TripViewModel_Id", "dbo.TripViewModels");
            DropForeignKey("dbo.TripViewModels", "Student_Id", "dbo.StuProfiles");
            DropForeignKey("dbo.TripViewModels", "Trip_Id", "dbo.Trips");
            DropIndex("dbo.Donations", new[] { "TripViewModel_Id" });
            DropIndex("dbo.TripViewModels", new[] { "Student_Id" });
            DropIndex("dbo.TripViewModels", new[] { "Trip_Id" });
            DropColumn("dbo.Donations", "TripViewModel_Id");
            DropTable("dbo.TripViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TripViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Student_Id = c.Int(),
                        Trip_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Donations", "TripViewModel_Id", c => c.Int());
            CreateIndex("dbo.TripViewModels", "Trip_Id");
            CreateIndex("dbo.TripViewModels", "Student_Id");
            CreateIndex("dbo.Donations", "TripViewModel_Id");
            AddForeignKey("dbo.TripViewModels", "Trip_Id", "dbo.Trips", "Id");
            AddForeignKey("dbo.TripViewModels", "Student_Id", "dbo.StuProfiles", "Id");
            AddForeignKey("dbo.Donations", "TripViewModel_Id", "dbo.TripViewModels", "Id");
        }
    }
}
