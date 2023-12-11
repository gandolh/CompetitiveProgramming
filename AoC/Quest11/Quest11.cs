

using BenchmarkDotNet.Reports;
using Microsoft.Diagnostics.Tracing.Parsers.JScript;
using System.Collections.Immutable;
using System.Numerics;

namespace AoC.Quest11
{
    /// <summary>
    /// Shortest path is hypothenuse of min of cathetes (so the minimum) * 2 steps
    /// - ( max - min) the straight line from end of hypothenuse to result
    ///  = min + max
    /// else straight line is shortest
    /// </summary>


    internal class Quest11 : BaseQuest
    {
        private readonly Direction UP = new Direction(0, -1);
        private readonly Direction DOWN = new Direction(0, 1);
        private readonly Direction LEFT = new Direction(-1, 0);
        private readonly Direction RIGHT = new Direction(1, 0);
        private readonly Direction WALL = new Direction(-99, -99);
        private readonly List<MatrixCoordinate> GalaxiesCoords = new();


        public override Task Solve()
        {
            string inPath = GetPathTo("quest11_1.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = File.ReadAllLines(inPath);
            File.WriteAllText(outPath, "");

            int n = lines.Length;
            int m = lines[0].Length;

            char[,] newMatrix = ConvertStringArrayToMatrix(lines);
            AddGalaxiesCoords(newMatrix, n, m);
            AddExpandedValues(newMatrix, n,m);

            Int64 sum = 0;
            for (int i = 0; i < GalaxiesCoords.Count; i++)
                for (int j = i + 1; j < GalaxiesCoords.Count; j++)
                {
                    Int64 distance = CalculateDistance(i, j);
                    //Console.WriteLine("{0}-{1} dist: {2}", i + 1, j + 1, distance);
                    sum += distance;
                }
            //PrintMatrix(newMatrix, n, m);
            Console.WriteLine(sum);
            return Task.CompletedTask;
        }

        private Int64 CalculateDistance(int i, int j)
        {
            if(i == j) return 0;
            Int64 Cathetus1 = Math.Abs(GalaxiesCoords[i].X - GalaxiesCoords[j].X);
            Int64 Cathetus2 = Math.Abs(GalaxiesCoords[i].Y - GalaxiesCoords[j].Y);
            if (Cathetus1 != 0 && Cathetus2 != 0)
            {
                Int64 min = Math.Min(Cathetus1, Cathetus2);
                Int64 max = Math.Max(Cathetus1, Cathetus2);

                return min+max;
            }
            else if (Cathetus1 != 0) return Cathetus1;
            else return Cathetus2;
        }

        private void AddGalaxiesCoords(char[,] matrix, int n, int m)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (matrix[i, j] == '#')
                        GalaxiesCoords.Add(new MatrixCoordinate(i, j));

        }

        private void AddExpandedValues(char[,] newMatrix, int n, int m)
        {

            //expansion rate - 1
            Int64 age = 1000000 - 1;

            List<List<char>> list = new List<List<char>>();
            List<MatrixCoordinate> originalCoords = GalaxiesCoords.Select(el=> new MatrixCoordinate(el.Y, el.X)).ToList();
            for (int i = 0; i < n; i++)
            {
                list.Add(new List<char>());
                for(int j=0; j < m;j++)
                    list[i].Add(newMatrix[i, j]);
            }


            for (int i = 0; i < n; i++)
            {
                if (list[i].All(x => x == '.'))
                {
                    for(int k=0; k < originalCoords.Count; k++)
                        if (originalCoords[k].Y > i)
                            GalaxiesCoords[k].Y = GalaxiesCoords[k].Y + age;
                }
            }


            for (int j = 0; j < m; j++)
            {
                if (list.All(x => x[j] == '.'))
                {
                    for (int k = 0; k < originalCoords.Count; k++)
                        if (originalCoords[k].X > j)
                            GalaxiesCoords[k].X = GalaxiesCoords[k].X + age;
                }
            }
        }

        private char[,] ConvertStringArrayToMatrix(string[] array)
        {
            char[,] matrix = new char[array.Length, array[0].Length];
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array[0].Length; j++)
                { matrix[i, j] = array[i][j]; }

            return matrix;
        }

        private void PrintMatrix(char[,] matrix, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    Console.Write(matrix[i, j]);
                Console.WriteLine();
            }
        }

        private void PrintMatrix(List<List<char>> matrix, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    Console.Write(matrix[i][j]);
                Console.WriteLine();
            }
        }
    }
}
