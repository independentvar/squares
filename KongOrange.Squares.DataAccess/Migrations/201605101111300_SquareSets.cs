namespace KongOrange.Squares.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SquareSets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SquareSets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.SquareSetPieces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        SquareSet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SquareSets", t => t.SquareSet_Id)
                .Index(t => t.SquareSet_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SquareSets", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SquareSetPieces", "SquareSet_Id", "dbo.SquareSets");
            DropIndex("dbo.SquareSetPieces", new[] { "SquareSet_Id" });
            DropIndex("dbo.SquareSets", new[] { "ApplicationUser_Id" });
            DropTable("dbo.SquareSetPieces");
            DropTable("dbo.SquareSets");
        }
    }
}
