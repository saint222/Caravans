namespace Caravans.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Factions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FactionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Squads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SquadName = c.String(),
                        MasterCard = c.Int(nullable: false),
                        Faction_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Factions", t => t.Faction_Id)
                .Index(t => t.Faction_Id);
            
            CreateTable(
                "dbo.Warriors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WarriorName = c.String(),
                        HP = c.Int(nullable: false),
                        AttackStrength = c.Int(nullable: false),
                        BlockStrength = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Squad_Id = c.Int(),
                        Faction_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Squads", t => t.Squad_Id)
                .ForeignKey("dbo.Factions", t => t.Faction_Id)
                .Index(t => t.Squad_Id)
                .Index(t => t.Faction_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Warriors", "Faction_Id", "dbo.Factions");
            DropForeignKey("dbo.Squads", "Faction_Id", "dbo.Factions");
            DropForeignKey("dbo.Warriors", "Squad_Id", "dbo.Squads");
            DropIndex("dbo.Warriors", new[] { "Faction_Id" });
            DropIndex("dbo.Warriors", new[] { "Squad_Id" });
            DropIndex("dbo.Squads", new[] { "Faction_Id" });
            DropTable("dbo.Warriors");
            DropTable("dbo.Squads");
            DropTable("dbo.Factions");
        }
    }
}
