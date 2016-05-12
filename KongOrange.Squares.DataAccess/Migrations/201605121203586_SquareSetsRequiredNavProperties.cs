namespace KongOrange.Squares.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SquareSetsRequiredNavProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SquareSetPieces", "SquareSet_Id", "dbo.SquareSets");
            DropForeignKey("dbo.SquareSets", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SquareSets", new[] { "User_Id" });
            DropIndex("dbo.SquareSetPieces", new[] { "SquareSet_Id" });
            RenameColumn(table: "dbo.SquareSetPieces", name: "SquareSet_Id", newName: "SquareSetId");
            RenameColumn(table: "dbo.SquareSets", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.SquareSets", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.SquareSetPieces", "SquareSetId", c => c.Int(nullable: false));
            CreateIndex("dbo.SquareSetPieces", "SquareSetId");
            CreateIndex("dbo.SquareSets", "UserId");
            AddForeignKey("dbo.SquareSetPieces", "SquareSetId", "dbo.SquareSets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SquareSets", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SquareSets", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SquareSetPieces", "SquareSetId", "dbo.SquareSets");
            DropIndex("dbo.SquareSets", new[] { "UserId" });
            DropIndex("dbo.SquareSetPieces", new[] { "SquareSetId" });
            AlterColumn("dbo.SquareSetPieces", "SquareSetId", c => c.Int());
            AlterColumn("dbo.SquareSets", "UserId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.SquareSets", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.SquareSetPieces", name: "SquareSetId", newName: "SquareSet_Id");
            CreateIndex("dbo.SquareSetPieces", "SquareSet_Id");
            CreateIndex("dbo.SquareSets", "User_Id");
            AddForeignKey("dbo.SquareSets", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.SquareSetPieces", "SquareSet_Id", "dbo.SquareSets", "Id");
        }
    }
}
