using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravans
{
    public class WarriorsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<WarriorsContext>
    {
        protected override void Seed(WarriorsContext context)
        {
            var warriorPresidents = new List<Warrior>
            {
                new Warrior ("Gerald Ford"),
                new Warrior("Ronald Reagan"),
                new Warrior ("Bill Klinton"),
                new Warrior ("George Bush"),
                new Warrior ("Barack Obama"),                
                new Warrior ("Donald Trump")                
            };
            
            var mySquadWest = new Squad { SquadName = "MySquadWest", MasterCard = 1000 };
            var squadPresibetsAlive = new Squad { SquadName = "AlivePresidents", MasterCard = 1000 };
            var squadPresibetsRIP = new Squad { SquadName = "AlivePresidentsRIP", MasterCard = 1000 };

            var factionPresidents = new Faction {FactionName = "WhiteHouseFaction", Warriors = warriorPresidents};

            factionPresidents.Squads = new List<Squad>();
            factionPresidents.Squads.Add(mySquadWest);
            factionPresidents.Squads.Add(squadPresibetsAlive);
            factionPresidents.Squads.Add(squadPresibetsRIP);

            var warriorSoltwort = new List<Warrior>
            {
                new Warrior ("Che Guevara"),
                new Warrior("Kim Jong Un"),
                new Warrior ("Saddam Hussein"),
                new Warrior ("Usame bin Ladin"),
                new Warrior ("Hassan Rouhani"),
                new Warrior ("Bashar Hafez al-Assad")
            };

            var mySquadEast = new Squad { SquadName = "MySquadEast", MasterCard = 1000 };
            var squadEastLeadersAlive = new Squad { SquadName = "EastLeadersAlive", MasterCard = 1000 };
            var squadEastLeadersRIP = new Squad { SquadName = "EastLeadersRIP", MasterCard = 1000 };

            var factionEasternLeaders = new Faction { FactionName = "EasternFaction", Warriors = warriorSoltwort };

            factionEasternLeaders.Squads = new List<Squad>();
            factionEasternLeaders.Squads.Add(mySquadEast);
            factionEasternLeaders.Squads.Add(squadEastLeadersAlive);
            factionEasternLeaders.Squads.Add(squadEastLeadersRIP);

            context.Factions.Add(factionPresidents);
            context.Factions.Add(factionEasternLeaders);

            context.SaveChanges();
        }
    }
}
