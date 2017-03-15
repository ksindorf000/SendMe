namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToTripCk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TripViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Student_Id = c.Int(),
                        Trip_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StuProfiles", t => t.Student_Id)
                .ForeignKey("dbo.Trips", t => t.Trip_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Trip_Id);
            
            AddColumn("dbo.Donations", "TripViewModel_Id", c => c.Int());
            CreateIndex("dbo.Donations", "TripViewModel_Id");
            AddForeignKey("dbo.Donations", "TripViewModel_Id", "dbo.TripViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TripViewModels", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.TripViewModels", "Student_Id", "dbo.StuProfiles");
            DropForeignKey("dbo.Donations", "TripViewModel_Id", "dbo.TripViewModels");
            DropIndex("dbo.TripViewModels", new[] { "Trip_Id" });
            DropIndex("dbo.TripViewModels", new[] { "Student_Id" });
            DropIndex("dbo.Donations", new[] { "TripViewModel_Id" });
            DropColumn("dbo.Donations", "TripViewModel_Id");
            DropTable("dbo.TripViewModels");
        }
    }
}
