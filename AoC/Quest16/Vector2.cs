using AoC.Data;

namespace AoC.Quest16
{
    internal class Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x,int y)
        {
            this.X = x;
            this.Y = y;
        }

        private Vector2()
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

        public static Vector2 operator +(Vector2 m1, Vector2 m2)
        {
          
            return new Vector2(m1.X + m2.X, m1.Y + m2.Y);
        }
    }
}
