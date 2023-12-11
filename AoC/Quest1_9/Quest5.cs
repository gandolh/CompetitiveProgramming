using AoC.Data;
using System.Text.RegularExpressions;

namespace AoC.Quest1_9
{
    internal class Quest5 : BaseQuest
    {
        private Regex _regex = new Regex(" +");
        private Dictionary<int, List<Vector3<long>>> Mappers = new();


        public override async Task Solve()
        {
            string inPath = GetPathTo("quest5_1.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = File.ReadAllLines(inPath);
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


            long minLocation = long.MaxValue;
            for (int i = 0; i < seeds.Count; i += 2)
            {
                for (long l = 0; l < seeds[i + 1]; l++)
                {
                    long seedResult = seeds[i] + l;
                    (seedResult, long skippingRange) = GetSeedResult(seedResult);
                    if (seedResult < minLocation)
                        minLocation = seedResult;
                    l = l + skippingRange - 1;
                }
            }

            //File.WriteAllText(outPath, minLocation.ToString());
            Console.WriteLine(minLocation);
        }

        private (long, long) GetSeedResult(long seedResult)
        {
            long skippingRange = long.MaxValue;
            for (int j = 0; j < Mappers.Count; j++)
            {

                for (int k = 0; k < Mappers[j].Count; k++)
                {
                    Vector3<long> mapper = Mappers[j][k];
                    if (seedResult >= mapper.Second && seedResult <= mapper.Second + mapper.Third - 1)
                    {
                        seedResult = mapper.First + (seedResult - mapper.Second);
                        long diff = mapper.First + mapper.Third - seedResult;
                        if (diff < skippingRange) skippingRange = diff;
                        break;
                    }
                }
            }
            return (seedResult, skippingRange);
        }

        private List<long> ToIntList(string numbers)
        {
            return _regex.Split(numbers).Select((el) => long.Parse(el)).ToList();
        }
    }
}
