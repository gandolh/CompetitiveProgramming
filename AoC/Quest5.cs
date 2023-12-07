using AoC.Data;
using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.Parsers.JScript;
using Microsoft.Diagnostics.Tracing.StackSources;
using System.Text.RegularExpressions;

namespace AoC
{
    internal class Quest5 : BaseQuest
    {
        private Regex _regex = new Regex(" +");
        private Dictionary<int, List<Vector3<long>>> Mappers = new();


        public override async Task Solve()
        {
            string inPath = GetPathTo("quest5_1.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = System.IO.File.ReadAllLines(inPath);
            int result = 0;
            File.WriteAllText(outPath, "");

            string[] splitArray = lines[0].Split("seeds: ");
            List<long> seeds = ToIntList(splitArray[1]);
            int step = -1;

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("map:"))
                {
                    step++;
                    Mappers[step] = new();
                    continue;
                }

                if (line == "") continue;

                var lineNumbers = ToIntList(line);
                Mappers[step].Add(new Vector3<long>(lineNumbers[0],
                    lineNumbers[1], lineNumbers[2]));
            }


            Int64 minLocation = Int64.MaxValue;
            Vector3<long>[] lastMapper = new Vector3<long>[7];
            for (int i = 0; i < seeds.Count; i += 2)
            {
                List<Task<long>> tasks = new();
                for (int l = 0; l < seeds[i + 1]; l++)
                {
                    Int64 seedResult = seeds[i] + l;
                    seedResult = GetSeedResult(seedResult, lastMapper);
                    if (seedResult < minLocation)
                        minLocation = seedResult;
                }
                // OR
                //Parallel.For(0, seeds[i + 1], (index) =>
                //{
                //    Int64 seedResult = seeds[i] + index;
                //    seedResult = GetSeedResult(seedResult, lastMapper);
                //    if (seedResult < minLocation)
                //        minLocation = seedResult;
                //});
            }

            File.WriteAllText(outPath, minLocation.ToString());

        }

        private long GetSeedResult(long seedResult, Vector3<long>[] lastMapper)
        {
            for (int j = 0; j < Mappers.Count; j++)
            {
                    if (lastMapper[j] != null && seedResult >= lastMapper[j].Second && seedResult <= (lastMapper[j].Second + lastMapper[j].Third - 1))
                    {
                        seedResult = lastMapper[j].First + (seedResult - lastMapper[j].Second);
                    }
                    else
                    {
                        for (int k = 0; k < Mappers[j].Count; k++)
                        {
                            Vector3<long> mapper = Mappers[j][k];
                            if (seedResult >= mapper.Second && seedResult <= (mapper.Second + mapper.Third - 1))
                            {
                                seedResult = mapper.First + (seedResult - mapper.Second);
                                lastMapper[j] = mapper;
                                break;
                            }
                        }
                }
            }
            return seedResult;
        }

        private List<Int64> ToIntList(string numbers)
        {
            return _regex.Split(numbers).Select((el) => Int64.Parse(el)).ToList();
        }
    }
}
