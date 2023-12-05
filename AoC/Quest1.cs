using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AoC
{
    /// <summary>
    /// We can do a lexical tree, but im lazy.
    /// </summary>
    internal class Quest1 : BaseQuest
    {
        private Dictionary<int, string> DigitStringMapper = new()
        {
            {1, "one" },
            {2, "two" },
            {3, "three" },
            {4, "four" },
            {5, "five" },
            {6, "six" },
            {7, "seven" },
            {8, "eight" },
            {9, "nine" },
        };

        public override Task Solve()
        {
            string inPath = GetPathTo("quest1_2.in");
            string outPath = GetPathTo("quest1_2.out");
            string[] lines = System.IO.File.ReadAllLines(inPath);
            File.WriteAllText(outPath, "");

            int sum = 0;
            foreach (string line in lines)
            {
                int firstEncounter = -1;
                int lastEncounter = -1;

                for (int i = 0; i < line.Length; i++)
                {
                    int number = -1;

                    if (line[i] >= '0' && line[i] <= '9')
                        number = line[i] - '0';
                    else
                        number = GetNumberIfFound(line, i);

                    if (number != -1)
                    {
                        if (firstEncounter == -1)
                            firstEncounter = number;
                        lastEncounter = number;
                    }
                }


                int caliber = (firstEncounter * 10 + lastEncounter);
                sum += caliber;
            }
            File.AppendAllText(outPath, sum.ToString());
            return Task.CompletedTask;
        }

        private int GetNumberIfFound(string line, int i)
        {
            for (int j = 1; j <= 9; j++)
            {
                int lineIndex = i;
                string toSearch = DigitStringMapper[j];
                bool matched = true;
                for (int k = 0; k < toSearch.Length; k++, lineIndex++)
                {
                    if (lineIndex >= line.Length || toSearch[k] != line[lineIndex])
                    { matched = false; break; }
                }

                if (matched)
                    return j;
            }
            return -1;
        }
    }
}
