using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravans
{
    public static class MyRandom
    {
        public static Random Rand { get; set; }

        static MyRandom() // статик-конструктор для рандома с генерацией разных чисел
        {
            Rand = new Random();
        }
    }
}
