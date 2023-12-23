using AoC.Data;

namespace AoC.Quest14
{
    internal class MatrixCoordinate
    {
        public Int64 X { get; set; }
        public Int64 Y { get; set; }

        public MatrixCoordinate(Int64 y, Int64 x)
        {
            this.X = x;
            this.Y = y;
        }

        private MatrixCoordinate()
        {
            
        }

        public Int64 this[int key]
        {
            get => GetValue(key);
        }

        private Int64 GetValue(int key)
        {
            if (key == 0) return X;
            if (key == 1) return Y;
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2<int> vector &&
                   EqualityComparer<Int64>.Default.Equals(X, vector.first) &&
                   EqualityComparer<Int64>.Default.Equals(Y, vector.second);
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
