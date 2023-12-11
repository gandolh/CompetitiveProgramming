using Microsoft.Diagnostics.Tracing.Parsers.JScript;
using System.Text.Json.Serialization;

namespace AoC.Quest1_9
{
    internal class Quest9 : BaseQuest
    {
        public override Task Solve()
        {
            string inPath = GetPathTo("quest9_1.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = File.ReadAllLines(inPath);
            File.WriteAllText(outPath, "");

            long sum = 0;

            foreach (string line in lines)
            {
                long nextItemInLine = 0;
                List<long> integers = ToInt(line);
                int seqLength = integers.Count();
                integers.Reverse();

                while (integers.Slice(0, seqLength).Any(x => x != 0))
                {
                    nextItemInLine += integers[seqLength - 1];
                    for (int i = 0; i < seqLength - 1; i++)
                        integers[i] = integers[i + 1] - integers[i];
                    seqLength--;
                }
                sum += nextItemInLine;
            }

            Console.WriteLine(sum);
            return Task.CompletedTask;
        }

        private List<long> ToInt(string str)
        {
            return str.Split(" ").Select(el => long.Parse(el)).ToList();
        }
    }
}
