namespace KongOrange.Squares.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SquareSetPieceNameRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SquareSetPieces", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SquareSetPieces", "Name", c => c.String(maxLength: 255));
        }
    }
}
