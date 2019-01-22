using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravans
{
    public class Warrior : BaseEntity
    {
        
        public string WarriorName { get; set; }
        public int HP { get; set; }
        public int AttackStrength { get; set; }
        public int BlockStrength { get; set; }
        public int Price { get; set; }
        public int? SquadId { get; set; } //

        /*Random Rnd = new Random(Guid.NewGuid().GetHashCode());*/ // Гайд с Хэшкодом закидывается внутрь,чтобы избежать повторяющихся генераций чисел
        public Warrior(string name)
        {            
            WarriorName = name;
            HP = 100;
            AttackStrength = MyRandom.Rand.Next(1, 100); // использование СТАТИК-КЛАССА для Рандома, чтоб генерил разные числа
            BlockStrength = MyRandom.Rand.Next(1, 100);
            Price = (AttackStrength+BlockStrength)/2;            
        }
        public Warrior()
        {
            
        }

        

    }
}
