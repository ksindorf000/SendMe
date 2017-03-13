namespace SendMe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStudentProfileAndStudentViewModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StuProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Bio = c.String(),
                        Year = c.String(),
                        Speciality = c.String(),
                        SchoolId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StuProfiles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.StuProfiles", new[] { "UserId" });
            DropTable("dbo.StuProfiles");
        }
    }
}
