namespace KongOrange.Squares.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SquareSetsNavigationProperties : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SquareSets", name: "ApplicationUser_Id", newName: "User_Id");
            RenameIndex(table: "dbo.SquareSets", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SquareSets", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.SquareSets", name: "User_Id", newName: "ApplicationUser_Id");
        }
    }
}
