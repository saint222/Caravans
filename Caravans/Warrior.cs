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
        public int WarriorPrice { get; set; }
    }
}
