namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeMistakeOnAttendances : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "Attendee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropIndex("dbo.Attendances", new[] { "Attendee_Id" });
            RenameColumn(table: "dbo.Attendances", name: "Attendee_Id", newName: "AttendeeId");
            DropPrimaryKey("dbo.Attendances");
            AlterColumn("dbo.Attendances", "AttendeeId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AttendeeId" });
            CreateIndex("dbo.Attendances", "AttendeeId");
            AddForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Attendances", "GigId", "dbo.Gigs", "Id");
            DropColumn("dbo.Attendances", "AtendeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendances", "AtendeeId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "AttendeeId" });
            DropPrimaryKey("dbo.Attendances");
            AlterColumn("dbo.Attendances", "AttendeeId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AtendeeId" });
            RenameColumn(table: "dbo.Attendances", name: "AttendeeId", newName: "Attendee_Id");
            CreateIndex("dbo.Attendances", "Attendee_Id");
            AddForeignKey("dbo.Attendances", "GigId", "dbo.Gigs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Attendances", "Attendee_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
