using AoC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
    internal class Quest3 : BaseQuest
    {
        private readonly int[] dirX = [0, 1, 1, 1, 0, -1, -1, -1];
        private readonly int[] dirY = [1, 1, 0, -1, -1, -1, 0, 1];
        private readonly Dictionary<Coordinate, List<int>> gearLocValue = new Dictionary<Coordinate, List<int>>();

        public override Task Solve()
        {
            string inPath = GetPathTo("quest3_1.in");
            string outPath = GetPathTo("quest3_2.out");
            string[] lines = System.IO.File.ReadAllLines(inPath);
            int sum = 0;
            File.WriteAllText(outPath, "");

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    int number = 0;
                    bool isPart = false;
                    Coordinate? starCoord = null;
                    while (j < lines[i].Length && lines[i][j] >= '0' && lines[i][j] <= '9')
                    {
                        Coordinate? foundStarCoord;
                        bool hasSymbolNeighbour;
                        number = number * 10 + (lines[i][j] - '0');
                        (hasSymbolNeighbour, foundStarCoord) = CheckNeighbours(lines, i, j);
                        if (hasSymbolNeighbour)
                        {
                            isPart = true;
                            if(foundStarCoord != null)
                                starCoord = foundStarCoord;
                        }
                        j++;
                    }


                    if (isPart && starCoord != null)
                    {
                        if (gearLocValue.ContainsKey(starCoord))
                            gearLocValue[starCoord].Add(number);
                        else
                            gearLocValue[starCoord] = new List<int>() { number };
                    }
                }
            }

            // do math for stars
            foreach (var entry in gearLocValue) {
                  if (entry.Value.Count  == 2)
                    sum = sum + entry.Value[0] * entry.Value[1];
            }

            return Task.CompletedTask;
            File.AppendAllText(outPath, sum.ToString());
        }

        // bool if found some character
        // Coordinate if found location of '*'
        private (bool, Coordinate?) CheckNeighbours(string[] lines, int i, int j)
        {
            bool found = false;
            Coordinate? coord = null;
            for (int k = 0; k < dirX.Length; k++)
            {
                if (i - dirX[k] < 0 || i - dirX[k] >= lines.Length 
                    || j - dirY[k] < 0 || j - dirY[k] >= lines.Length)
                    continue;
                char visitedNeighbour = lines[i - dirX[k]][j - dirY[k]];
                if (visitedNeighbour != '.' && (visitedNeighbour < '0' || visitedNeighbour > '9'))
                {
                    if (visitedNeighbour == '*')
                        coord = new Coordinate(i - dirX[k], j - dirY[k]);
                    found =  true;
                }
            }
            return (found, coord);
        }
    }
}
