using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC.Quest1_9
{
    internal class Quest6 : BaseQuest
    {
        private Regex _regex = new Regex(" +");
        public override Task Solve()
        {
            string inPath = GetPathTo("quest6_1.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = File.ReadAllLines(inPath);
            long result = 1;
            File.WriteAllText(outPath, "");

            long times = ToIntRemoveSpaces(lines[0].Split("Time:")[1].Trim());
            long distances = ToIntRemoveSpaces(lines[1].Split("Distance:")[1].Trim());
            int nr = 0;
            bool foundOne = false;
            for (int j = 0; j < times; j++)
            {
                if (j * (times - j) > distances)
                {
                    nr++;
                    foundOne = true;
                }
                else if (foundOne == true)
                    break;
            }
            result = nr;


            File.WriteAllText(outPath, result.ToString());

            return Task.CompletedTask;
        }


        private long[] ToIntList(string numbers)
        {
            return _regex.Split(numbers).Select((el) => long.Parse(el)).ToArray();
        }

        private long ToIntRemoveSpaces(string number)
        {
            return long.Parse(number.Replace(" ", ""));
        }
    }
}
