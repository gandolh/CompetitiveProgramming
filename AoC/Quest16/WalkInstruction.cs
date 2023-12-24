
namespace AoC.Quest16
{
    internal class WalkInstruction
    {
        public MatrixCoordinate coordinates;
        public MatrixDirection Direction;

        public WalkInstruction(int coordX, int coordY, MatrixDirection md)
        {
            coordinates = new(coordX, coordY);
            Direction = md;
        }

        public override bool Equals(object? obj)
        {
            return obj is WalkInstruction instruction &&
                   EqualityComparer<MatrixCoordinate>.Default.Equals(coordinates, instruction.coordinates) &&
                   EqualityComparer<MatrixDirection>.Default.Equals(Direction, instruction.Direction);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(coordinates, Direction);
        }
    }
}
