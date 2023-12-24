using AoC.Quest16;
using Microsoft.Diagnostics.Tracing;
using System.Data;

namespace AoC.Quest18
{
    internal class Quest18 : BaseQuest
    {
        private readonly MatrixDirection UP = new MatrixDirection(-1, 0);
        private readonly MatrixDirection RIGHT = new MatrixDirection(0, 1);
        private readonly MatrixDirection DOWN = new MatrixDirection(1, 0);
        private readonly MatrixDirection LEFT = new MatrixDirection(0, -1);
        private readonly MatrixDirection NONE = new MatrixDirection(-99, -99);
        char[][] Map = [];
        private const int _maxWidth = 10;
        private const int _maxHeight = 10;
        public override Task Solve()
        {
            string inPath = GetPathTo("quest18_1.in");
            string outPath = GetPathTo("questResult.out");
            File.WriteAllText(outPath, "");
            string[] lines = File.ReadAllLines(inPath);
            InitMap();
            MatrixCoordinate currentPos = new MatrixCoordinate(0,0);
            Map[0][0] = '#';
            foreach (var line in lines)
            {
                string[] splitArray = line.Split(' ');
                char dirChar = splitArray[0][0];
                string valueStr = splitArray[1];
                MatrixDirection dir = GetDirection(dirChar);
                int value = Int32.Parse(valueStr);
                for(int i = 0;i < value; i++)
                {
                    currentPos.Add(dir);
                    Map[currentPos.X][currentPos.Y] = '#';
                }
            }

           
            //int borderResult = GetDiezCount(Map, _maxHeight, _maxWidth);
            //Console.WriteLine(borderResult);
            FillShape(Map, _maxHeight, _maxWidth);
            //Print(Map, _maxHeight, _maxWidth);
            int shapeResult = GetDiezCount(Map, _maxHeight, _maxWidth);
            Console.WriteLine(shapeResult);
            return Task.CompletedTask;
        }

        private void FillShape(char[][] map, int maxWidth, int maxHeight)
        {
            for(int i=0;i< maxHeight; i++)
                for(int j=0;j<maxWidth; j++)
                    if (map[i][j] == '.')
                    {
                        bool a = FindDiezOnleft(map, i, j);
                        bool b = FindDiezOnRight(map, i, j);
                        if (a && b) map[i][j] = '#';
                    }
        }

        private bool FindDiezOnRight(char[][] map, int i, int j)
        {
            for(int k = j;k<_maxWidth; k++)
            {
                if (map[i][k] == '#')
                    return true;   
            }
            return false;
        }

        private bool FindDiezOnleft(char[][] map, int i, int j)
        {
            for (int k = j; k >=0; k--)
            {
                if (map[i][k] == '#')
                    return true;
            }
            return false;
        }

        private void InitMap()
        {
            
            Map = new char[_maxHeight][];
            for (int i = 0; i < _maxHeight; i++)
            {
                Map[i] = new char[_maxWidth];
                for (int j = 0; j < _maxWidth; j++)
                {
                    Map[i][j] = '.';
                }
            }
        }
        private MatrixDirection GetDirection(char dirChar)
        {
            if (dirChar == 'U') return UP;
            if (dirChar == 'R') return RIGHT;
            if (dirChar == 'D') return DOWN;
            if (dirChar == 'L') return LEFT;
            return NONE;
        }
        private int GetDiezCount(char[][] map, int n, int m)
        {
            int count = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (map[i][j] == '#')
                        count++;
            return count;
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
