using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravans
{
    public class Squad : BaseEntity
    {
        public string SquadName { get; set; }
        public virtual List<Warrior> Warriors { get; set; }
    }
}
