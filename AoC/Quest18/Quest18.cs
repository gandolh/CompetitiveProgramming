using AoC.Quest16;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
using System.Data;

namespace AoC.Quest18
{
    // DOING SHOELACE FORMULA and Picks Theorem
    // i can do it more ram efficient by not saving every point and saving a total number of boundaries
    internal class Quest18 : BaseQuest
    {
        private readonly MatrixDirection UP = new MatrixDirection(-1, 0);
        private readonly MatrixDirection RIGHT = new MatrixDirection(0, 1);
        private readonly MatrixDirection DOWN = new MatrixDirection(1, 0);
        private readonly MatrixDirection LEFT = new MatrixDirection(0, -1);
        private readonly MatrixDirection NONE = new MatrixDirection(-99, -99);

        private readonly List<MatrixCoordinate> ShapeCoordinates = new List<MatrixCoordinate>();
        public override Task Solve()
        {
            string inPath = GetPathTo("quest18_1.in");
            string outPath = GetPathTo("questResult.out");
            File.WriteAllText(outPath, "");
            string[] lines = File.ReadAllLines(inPath);
            MatrixCoordinate lastCoordinate = new MatrixCoordinate(0, 0);
            ShapeCoordinates.Add(lastCoordinate);
            foreach (var line in lines)
            {
                string[] splitArray = line.Split(' ');
                string hex = splitArray[2].Replace("(","").Replace(")","").Replace("#","");
                
                
                char dirChar = IntToDirChar(Int32.Parse(hex[5].ToString()));
                MatrixDirection dir = GetDirection(dirChar);
                int value = HexToInt(hex);
                for(int i=0; i<value; i++)
                {
                    MatrixCoordinate newCoordinate = lastCoordinate.AddWithClone(dir);
                    ShapeCoordinates.Add(newCoordinate);
                    lastCoordinate = newCoordinate;
                }
            }

            double area = getShoelaceArea();
            long boundaries = ShapeCoordinates.Count;
            long interior = (long)area - boundaries/2 + 1;
            long resultArea = interior + boundaries - 1;
            Console.WriteLine(resultArea);
            //Console.WriteLine(shapeResult);
            return Task.CompletedTask;
        }

        private int HexToInt(string hex)
        {
            string value = hex.Substring(0, 5);
            return Convert.ToInt32("0x" + value, 16);
        }

        private char IntToDirChar(int v)
        {
            switch (v)
            {
                case 0:
                    return 'R';
                case 1:
                    return 'D';
                case 2:
                    return 'L';
                case 3:
                    return 'U';
                default:
                    return '\0';
            }
        }

        private double getShoelaceArea()
        {
            double sum = 0;
            for(int i = 0;  i < ShapeCoordinates.Count; i++)
            {
                MatrixCoordinate prev;
                MatrixCoordinate next;
                if (i == 0)
                    prev = ShapeCoordinates.Last();
                else
                    prev= ShapeCoordinates[i - 1];
                
                if(i == ShapeCoordinates.Count - 1)
                    next = ShapeCoordinates.First();
                else
                    next = ShapeCoordinates[i + 1];
                sum = sum + ShapeCoordinates[i].X * (next.Y - prev.Y);
            }

            return Math.Abs(sum) / 2;
        }

        private MatrixDirection GetDirection(char dirChar)
        {
            if (dirChar == 'U') return UP;
            if (dirChar == 'R') return RIGHT;
            if (dirChar == 'D') return DOWN;
            if (dirChar == 'L') return LEFT;
            return NONE;
        }
        private void Print(char[][] mat, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (mat[i][j] == 0)
                        Console.Write('.');
                    Console.Write(mat[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}
