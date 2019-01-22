namespace Caravans.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Warriors", name: "Squad_Id", newName: "SquadId");
            RenameIndex(table: "dbo.Warriors", name: "IX_Squad_Id", newName: "IX_SquadId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Warriors", name: "IX_SquadId", newName: "IX_Squad_Id");
            RenameColumn(table: "dbo.Warriors", name: "SquadId", newName: "Squad_Id");
        }
    }
}
