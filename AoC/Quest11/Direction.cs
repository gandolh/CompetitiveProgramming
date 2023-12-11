using AoC.Data;
using AoC.Quesint0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Quest11
{
    internal class Direction
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Direction(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        private Direction()
        {

        }

        public int this[int key]
        {
            get => GetValue(key);
        }

        private int GetValue(int key)
        {
            if (key == 0) return X;
            if (key == 1) return Y;
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2<int> vector &&
                   EqualityComparer<int>.Default.Equals(X, vector.first) &&
                   EqualityComparer<int>.Default.Equals(Y, vector.second);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static Direction operator +(Direction m1, Direction m2)
        {

            return new Direction(m1.X + m2.X, m1.Y + m2.Y);
        }

    }
}
