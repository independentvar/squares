namespace KongOrange.Squares.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SquareSetPiecesRequiredProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SquareSetPieces", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.SquareSetPieces", "ImageUrl", c => c.String(nullable: false));
            AlterColumn("dbo.SquareSets", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SquareSets", "Name", c => c.String());
            AlterColumn("dbo.SquareSetPieces", "ImageUrl", c => c.String());
            AlterColumn("dbo.SquareSetPieces", "Name", c => c.String());
        }
    }
}
