using AoC.Data;

namespace AoC.Quesint0
{
    internal class MatrixCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MatrixCoordinate(int first, int second)
        {
            this.X = first;
            this.Y = second;
        }

        private MatrixCoordinate()
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

        public static MatrixCoordinate operator +(MatrixCoordinate m1, MatrixCoordinate m2)
        {
          
            return new MatrixCoordinate(m1.X + m2.X, m1.Y + m2.Y);
        }
    }
}
