
namespace AoC.Quest16
{
    internal class MatrixCoordinate : Vector2
    {
        public MatrixCoordinate(int x, int y) : base(x,y) { }

        public override bool Equals(object? obj)
        {
            return obj is MatrixCoordinate coordinate &&
                   X == coordinate.X &&
                   Y == coordinate.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), X, Y);
        }

        internal void Add(MatrixDirection dir)
        {
           this.X += dir.X;
            this.Y += dir.Y;
        }

        internal MatrixCoordinate AddWithClone(MatrixDirection dir)
        {
          return  new MatrixCoordinate(this.X + dir.X, this.Y + dir.Y);
        }
    }
}
