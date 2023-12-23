using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Quest16
{
    internal class MatrixDirection : Vector2
    {
        public MatrixDirection(int x, int y) : base(x, y) { }

        public override bool Equals(object? obj)
        {
            bool equals =  obj is MatrixDirection direction &&
                   X == direction.X &&
                   Y == direction.Y;
            return equals;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), X, Y);
        }
    }
}
