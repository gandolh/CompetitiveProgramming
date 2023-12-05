using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace AoC
{
    internal class Quest5 : BaseQuest
    {
        private Regex _regex = new Regex(" +");
        private List<Dictionary<Int64, Int64>> Mappers = new();


        public override void Solve()
        {
            string inPath = GetPathTo("quest5_1.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = System.IO.File.ReadAllLines(inPath);
            int result = 0;
            File.WriteAllText(outPath, "");

            string[] splitArray = lines[0].Split("seeds: ");
            List<Int64> seeds = ToIntList(splitArray[1]);
            int step = -1;

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains("map:"))
                {
                    step++;
                    Mappers.Add(new Dictionary<Int64, Int64>());
                    continue;
                }

                if (line == "") continue;

                List<Int64> lineNumbers = ToIntList(line);
                for (int j = 0; j < lineNumbers[2]; j++)
                {
                    Mappers[step][lineNumbers[1] + j] 
                }
            }


            Int64 minLocation = Int64.MaxValue;
            for( int i = 0; i< seeds.Count;i++)
            {
                Int64 seedResult = seeds[i];
                for (int j = 0; j < Mappers.Count; j++)
                {
                    Int64 tmpResult = -1;
                    var found = Mappers[j].TryGetValue(seedResult, out tmpResult);
                    if (found) seedResult = tmpResult;
                }
                if(seedResult < minLocation) minLocation = seedResult;
            }

            File.WriteAllText(outPath, minLocation.ToString());

        }

        private List<Int64> ToIntList(string numbers)
        {
            return _regex.Split(numbers).Select((el) => Int64.Parse(el)).ToList();
        }
    }
}
