using AoC.Quest13;

namespace AoC.Quest14
{
    internal class Quest14 : BaseQuest
    {
        private const char RoundRock = 'O';

        public override Task Solve()
        {
            string inPath = GetPathTo("quest14_1.in");
            string outPath = GetPathTo("questResult.out");
            File.WriteAllText(outPath, "");
            char[][] lines = File.ReadAllLines(inPath).ConvertToCharArray();
            int n = lines.Length;
            int m = lines[0].Length;
            for (int i = 0; i < 1_000_000_000; i++)
            {
                Console.WriteLine($"{i}/{1_000_000_000}");
                GoFullCycle(lines,n,m);
            }

            long result = calcLoad(lines, n, m);
            Console.WriteLine(result);
            return Task.CompletedTask;
        }

        private void GoFullCycle(char[][] lines, int n, int m)
        {
            FlipToNorth(lines, n, m);
            FlipToWest(lines, n, m);
            FlipToSouth(lines, n, m);
            FlipToEast(lines, n, m);
        }

        private long calcLoad(char[][] lines, int n, int m)
        {
            long sum = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    if (lines[i][j] == RoundRock)
                        sum = sum + n - i;
                }
            return sum;
        }
        private void Print(char[][] lines, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    Console.Write(lines[i][j]);
                Console.WriteLine();
            }
        }

        private void FlipToNorth(char[][] lines, int n, int m)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    if (lines[i][j] == RoundRock)
                    {
                        MoveToTop(lines, i, j);
                    }
                }
        }

        private void FlipToEast(char[][] lines, int n, int m)
        {
            for (int i = 0; i < n; i++)
                for (int j = m - 1; j >= 0; j--)
                {
                    if (lines[i][j] == RoundRock)
                    {
                        MoveToRight(lines, i, j, m);
                    }
                }
        }

        private void FlipToSouth(char[][] lines, int n, int m)
        {
            for (int i = n - 1; i >= 0; i--)
                for (int j = 0; j < m; j++)
                {
                    if (lines[i][j] == RoundRock)
                    {
                        MoveToDown(lines, i, j, n);
                    }
                }
        }

        private void FlipToWest(char[][] lines, int n, int m)
        {
            for (int i = 0; i < n; i++)
                for (int j = 1; j < m; j++)
                {
                    if (lines[i][j] == RoundRock)
                    {
                        MoveToLeft(lines, i, j);
                    }
                }
        }



        private void MoveToTop(char[][] lines, int i, int j)
        {
            int lastAvailableRow = i;
            while (lastAvailableRow > 0 && lines[lastAvailableRow - 1][j] == '.')
                lastAvailableRow--;

            lines[i][j] = '.';
            lines[lastAvailableRow][j] = RoundRock;
        }

        private void MoveToRight(char[][] lines, int i, int j, int m)
        {
            int lastAvailableCol = j;
            while (lastAvailableCol < m - 1 && lines[i][lastAvailableCol + 1] == '.')
                lastAvailableCol++;

            lines[i][j] = '.';
            lines[i][lastAvailableCol] = RoundRock;
        }

        private void MoveToDown(char[][] lines, int i, int j, int n)
        {
            int lastAvailableRow = i;
            while (lastAvailableRow < n - 1 && lines[lastAvailableRow + 1][j] == '.')
                lastAvailableRow++;

            lines[i][j] = '.';
            lines[lastAvailableRow][j] = RoundRock;
        }

        private void MoveToLeft(char[][] lines, int i, int j)
        {
            int lastAvailableCol = j;
            while (lastAvailableCol > 0 && lines[i][lastAvailableCol - 1] == '.')
                lastAvailableCol--;

            lines[i][j] = '.';
            lines[i][lastAvailableCol] = RoundRock;
        }


        private int CompareCharMatrix(char[][] lines, char[][] initialLines, int n, int m)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (lines[i][j] > initialLines[i][j]) return 1;
                    else if (lines[i][j] < initialLines[i][j]) return -1;
            return 0;
        }

        private char[][] DeepCloneCharMatrix(char[][] lines, int n, int m)
        {
            char[][] chars = new char[n][];
            for (int i = 0; i < n; i++)
            {
                chars[i] = new char[m];
                for (int j = 0; j < m; j++)
                    chars[i][j] = lines[i][j];
            }
            return chars;
        }

        private string ConvertCharMatrixToString(char[][] mat)
        {
            string str = "";
            for(int i=0;i<mat.Length; i++)
                string.Join("",str, string.Join("", mat[i]));
            return str;
        }

       
    }
}
