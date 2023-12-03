using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
    internal class Quest2 : BaseQuest
    {
        private readonly int maxRed = 12;
        private readonly int maxGreen = 13;
        private readonly int maxBlue = 14;

        public override void Solve()
        {
            string inPath = GetPathTo("quest2_1.in");
            string outPath = GetPathTo("quest2_2.out");
            string[] lines = System.IO.File.ReadAllLines(inPath);
            File.WriteAllText(outPath, "");


            //FirstSubProblem(lines, outPath);
            SecondSubProblem(lines, outPath);
        }

        private void SecondSubProblem(string[] lines, string outPath)
        {
            int sum = 0;

            foreach (string line in lines)
            {
                string[] splitArray = line.Split(':');
                (string gameIdentifier, string formattedLine) = (splitArray[0], splitArray[1]);
                int gameId = getNumberStartingFrom(gameIdentifier, 5);

                string[] games = formattedLine.Split(";");
                int maxR = 0;
                int maxG = 0;
                int maxB = 0;

                for (int i = 0; i < games.Length; i++)
                {
                    (int r, int g, int b) = extractRgbFromGame(games[i]);
                    if(r > maxR) maxR = r;
                    if(g > maxG) maxG = g;
                    if(b > maxB) maxB = b;
                }
                int power = maxR * maxG * maxB;
                sum = sum + power;

            }
            File.AppendAllText(outPath, sum.ToString());
        }



        private void FirstSubProblem(string[] lines, string outPath)
        {
            int sum = 0;

            foreach (string line in lines)
            {
                string[] splitArray = line.Split(':');
                (string gameIdentifier, string formattedLine) = (splitArray[0], splitArray[1]);
                int gameId = getNumberStartingFrom(gameIdentifier, 5);

                string[] games = formattedLine.Split(";");
                bool isPossible = true;

                for (int i = 0; i < games.Length; i++)
                {
                    (int r, int g, int b) = extractRgbFromGame(games[i]);
                    if (r > maxRed || g > maxGreen || b > maxBlue)
                    { isPossible = false; break; }
                }

                if (isPossible) sum = sum + gameId;

            }
            File.AppendAllText(outPath, sum.ToString());
        }
        (int r, int g, int b) extractRgbFromGame(string game)
        {
            int r, g, b;
            r = g = b = 0;
            string[] cubes = game.Split(",");
            for (int i = 0; i < cubes.Length; i++)
            {
                int number = getNumberStartingFrom(cubes[i], 1);
                if (cubes[i].Contains("red"))
                    r = number;
                if (cubes[i].Contains("green"))
                    g = number;
                if (cubes[i].Contains("blue"))
                    b = number;
            }

            return (r, g, b);
        }
        int getNumberStartingFrom(string str, int index)
        {
            int number = 0;
            while (index < str.Length && str[index] >= '0' && str[index] <= '9')
            {
                number = number * 10 + (str[index] - '0');
                index++;
            }
            return number;
        }
    }
}
