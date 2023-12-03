using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Data
{
    internal class Coordinate : MyPair<int, int>
    {
        public Coordinate(int first, int second) : base(first, second)
        {
        }
    }
}
