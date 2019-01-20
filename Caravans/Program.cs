using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Caravans
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new WarriorsContext())
            {
                Console.WriteLine("Here is a list of factions:");                
                var faction = context.Factions.Include(i => i.Warriors);/*FirstOrDefault(x => x.Id == 1)*/;
                foreach (var i in faction)
                {
                    Console.WriteLine($"{i.Id}-{i.FactionName} ");
                }
                Console.WriteLine("Choose a faction to look through it's warriors...");

                var factionId = Int32.Parse(Console.ReadLine());
                if (factionId != 0)
                {
                    var result = context.Factions.Find(factionId);
                    if (result != null)
                    {
                        Console.WriteLine("Here is the list of warriors:");
                        foreach (var i in result.Warriors)
                        {
                            Console.WriteLine($"{i.Id}-{i.WarriorName} with {i.HP} HP, {i.Price} $ cost, {i.AttackStrength} points of attack power and {i.BlockStrength} points security abilities.");
                        }
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
