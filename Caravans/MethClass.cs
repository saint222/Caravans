using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Caravans
{
    public class MethClass
    {
        public void Menu()
        {
            Console.Clear();
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Dear user! Please, make your choice and press Enter...");
                Console.WriteLine("Press" + " 1 " + "to look through a squad's warriors.");
                Console.WriteLine("Press" + " 2 " + "to create a new warrior.");
                Console.WriteLine("Press" + " 3 " + "to buy a warrior for a squad.");
                Console.WriteLine("Press" + " 4 " + "to attack a caravan.");
                Console.WriteLine("Press" + " 5 " + "to exit.");

                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        ShowSquadWarriors();
                        break;
                    case "2":
                        AddWarrior();
                        break;
                    case "3":
                        BuyWarrior();
                        break;
                    case "4":
                        AttackCaravan();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("wrong input");
                        break;
                }
            }
            Console.ReadLine();
        }

        public void AddWarrior()
        {
            Console.Clear();
            Console.WriteLine("Enter a name of a new warrior and press Enter...");
            var warrior = new Warrior(Console.ReadLine()); // стринги в круглых скобках вызовет КОНСТРУКТОР
            var correct = false;
            while (!correct)
            {
                Console.WriteLine("Here is a list of factions, choose a faction to add a warrior and press Enter...");
                using (var context = new WarriorsContext())
                {
                    var faction = context.Factions;
                    foreach (var fact in faction)
                    {
                        Console.WriteLine($"{fact.Id}-{fact.FactionName} ");
                    }

                    var result = Int32.Parse(Console.ReadLine());
                    if (result != 0)
                    {
                        var chosenFaction = context.Factions.Find(result);
                        if (chosenFaction != null)
                        {
                            chosenFaction.Warriors.Add(warrior);
                            context.SaveChanges();
                            Console.WriteLine("Done...");
                            correct = true;
                        }
                        else
                        {
                            Console.WriteLine("There is no a faction with the chosen Id...");
                            correct = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input...");
                        correct = false;
                    }
                    Console.ReadLine();
                }
            }
        }

        public void BuyWarrior()
        {
            Console.Clear();
            var correct = false;
            while (!correct)
            {
                Console.WriteLine("Here is a list of warriors. Choose a warrior to buy and press enter...");
                using (var context = new WarriorsContext())
                {
                    var faction = context.Factions.Include(i => i.Warriors).ToList();
                    foreach (var p in faction) // шагаем форычем по сложному объекту
                    {
                        foreach (var i in p.Warriors) // шагаем форычем по сложному объекту
                        {
                            Console.WriteLine($"{i.Id}-{i.WarriorName} with {i.HP} HP, {i.Price} $ cost, {i.AttackStrength} points of attack power and {i.BlockStrength} points security abilities.");
                        }
                    }
                    var result = Int32.Parse(Console.ReadLine());
                    if (result != 0)
                    {
                        var chosenWarrior = context.Warriors.Find(result);
                        if (chosenWarrior != null)
                        {
                            Console.WriteLine("Choose a squad for a bought warrior and press Enter...");
                            var squad = context.Squads;
                            foreach (var s in squad)
                            {
                                Console.WriteLine($"{s.Id}-Squad {s.SquadName} with it's treasury of {s.MasterCard} $;");
                            }
                            var result_1 = Int32.Parse(Console.ReadLine());
                            if (result_1 != 0)
                            {
                                var chosenSquad = context.Squads.Find(result_1);
                                if (chosenSquad != null && chosenSquad.MasterCard >= chosenWarrior.Price)
                                {
                                    chosenSquad.Warriors.Add(chosenWarrior);
                                    chosenSquad.MasterCard = chosenSquad.MasterCard - chosenWarrior.Price;
                                    context.SaveChanges();
                                    Console.WriteLine("Done!");
                                    Console.WriteLine($"There are {chosenSquad.MasterCard} $ in {chosenSquad.SquadName} treasury left.");
                                    correct = true;
                                }
                                else
                                {
                                    Console.WriteLine("There is mo a squad with such Id or you don't have enough money to buy the chosen warrior...");
                                    correct = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Incorrect input...");
                                correct = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no a warrior with such Id...");
                            correct = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input...");
                        correct = false;
                    }
                    Console.ReadLine();
                }
            }
        }

        public void ShowSquadWarriors()
        {
            var correct = false;
            while (!correct)
            {
                Console.Clear();
                Console.WriteLine("Here is a list of squads, choose one to look through it's warriors...");
                using (var context = new WarriorsContext())
                {
                    var squads = context.Squads;
                    foreach (var i in squads)
                    {
                        Console.WriteLine($"{i.Id}-{i.SquadName} with {i.MasterCard} $ in the treasury.");
                    }
                    var result = Int32.Parse(Console.ReadLine());
                    if (result != 0)
                    {
                        var chosenSquad = context.Squads.Find(result);
                        if (chosenSquad != null)
                        {
                            Console.WriteLine($"The warriors from the {chosenSquad.SquadName} are:");
                            foreach (var p in chosenSquad.Warriors)
                            {
                                Console.WriteLine($"{p.Id}-{p.WarriorName} with {p.HP} HP, {p.Price} $ cost, {p.AttackStrength} points of attack power and {p.BlockStrength} points security abilities.");
                            }
                            Console.WriteLine($"There are {chosenSquad.MasterCard} $ in {chosenSquad.SquadName} treasure.");
                            correct = true;
                        }
                        else
                        {
                            Console.WriteLine("There is no a squad with such Id...");
                            correct = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input...");
                        correct = false;
                    }
                    Console.ReadLine();
                }
            }
        }
        public void AttackCaravan()
        {
            using (var context = new WarriorsContext())
            {
                Squad chosenSquad_1;
                Squad chosenSquad_2;
                Console.WriteLine("Choose the first squad to fight");
                var squad_1 = context.Squads;
                foreach (var i in squad_1)
                {
                    Console.WriteLine($"{i.Id}-{i.SquadName} with {i.MasterCard} $ in the treasury.");
                }

                var result_1 = Int32.Parse(Console.ReadLine());

                if (result_1 != 0)
                {
                    chosenSquad_1 = context.Squads.Find(result_1);
                    Console.WriteLine("Choose the second squad to fight");
                    var squad_2 = context.Squads;
                    foreach (var i in squad_2)
                    {
                        Console.WriteLine($"{i.Id}-{i.SquadName} with {i.MasterCard} $ in the treasury.");
                    }
                    var result_2 = Int32.Parse(Console.ReadLine());
                    if (result_2 != 0)
                    {
                        chosenSquad_2 = context.Squads.Find(result_2);
                        var figter_1 = chosenSquad_1.Warriors.FirstOrDefault();
                        var figter_2 = chosenSquad_2.Warriors.FirstOrDefault();
                        Console.WriteLine($"{figter_1.WarriorName} is going kick {figter_2.WarriorName}'s ass...press Enter to start fighting...");
                        Console.ReadLine();
                        int conditionFirst = figter_1.HP;
                        int conditionSecond = figter_2.HP;
                        Random rnd = new Random();                       
                        while (figter_1.HP > 0 || figter_2.HP > 0)
                        {
                            int damage_1;
                            int damge_2;
                            damage_1 = figter_2.AttackStrength - figter_1.BlockStrength;
                            
                            if (damage_1 > 10)
                            {
                                conditionFirst = conditionFirst - damage_1;
                            }
                            else if (damage_1 < 10)
                            {
                                conditionFirst = conditionFirst - (rnd.Next (1,6));
                            }

                            damge_2 = figter_1.AttackStrength - figter_2.BlockStrength;

                            if (damge_2 > 10)
                            {
                                conditionSecond = conditionSecond - damge_2;
                            }
                            else if (damage_1 < 10)
                            {
                                conditionSecond = conditionSecond - (rnd.Next(1, 6));
                            }

                            if (conditionFirst == 0)
                            {
                                Console.WriteLine($"{figter_1.WarriorName} has won...");
                                Console.WriteLine($"{figter_2.WarriorName} has lost...");
                            }
                            else if (conditionSecond == 0)
                            {
                                Console.WriteLine($"{figter_2.WarriorName} has won...");
                                Console.WriteLine($"{figter_1.WarriorName} has lost...");
                            }                            
                        }
                    }
                }
                context.SaveChanges();
                Console.ReadLine();
            }
        }

        public void SomeMeth()
        {
            using (var context = new WarriorsContext())
            {
                Console.WriteLine("Here is a list of factions:");
                var faction = context.Factions.Include(i => i.Warriors);/*FirstOrDefault(x => x.Id == 1)*/;
                foreach (var i in faction)
                {
                    Console.WriteLine($"{i.Id}-{i.FactionName} ");
                }
                Console.WriteLine("Choose a faction to look through it's squads...");
                var factionId_0 = Int32.Parse(Console.ReadLine());
                if (factionId_0 != 0)
                {
                    var result_0 = context.Factions.Find(factionId_0);
                    foreach (var i in result_0.Squads)
                    {
                        Console.WriteLine($"{i.Id}-Squad {i.SquadName} with it's treasury of {i.MasterCard} $;");
                    }
                }
                Console.WriteLine("Choose a squad to add a warrior to...");

                var squadId = Int32.Parse(Console.ReadLine());
                Squad chosenSquad; // промежуточная переменная для связи с отрядом
                if (squadId != 0)
                {
                    chosenSquad = context.Squads.Find(squadId);
                    Console.WriteLine("Choose a faction to look through the warriors to buy...");
                    foreach (var i in faction)
                    {
                        Console.WriteLine($"{i.Id}-{i.FactionName} ");
                    }

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
                    Console.WriteLine("Choose a warrior to buy...");
                    var warriorId = Int32.Parse(Console.ReadLine());
                    if (warriorId != 0)
                    {

                        var result_1 = context.Warriors.Find(warriorId);
                        if (result_1 != null && chosenSquad.MasterCard >= result_1.Price)
                        {
                            var chosenWarrior = context.Warriors.Where(x => x.Id == warriorId).Select(x => x).FirstOrDefault();
                            chosenSquad.Warriors.Add(chosenWarrior);
                            chosenSquad.MasterCard = chosenSquad.MasterCard - result_1.Price;
                            context.SaveChanges();
                        }
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
