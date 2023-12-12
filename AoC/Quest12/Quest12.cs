using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Quest12
{
    internal class Quest12 : BaseQuest
    {
        public override Task Solve()
        {
            string inPath = GetPathTo("quest12_1.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = File.ReadAllLines(inPath);
            File.WriteAllText(outPath, "");
            Int64 sum = 0;

            foreach (string line in lines)
            {
                string[] splitArray = line.Split(' ');
                string sequence = splitArray[0];
                int[] numbers = splitArray[1].Split(',').Select(Int32.Parse).ToArray();
                string newSequence = string.Join("?",Enumerable.Repeat(sequence, 5));
                int[] newNumbers = Enumerable.Repeat(numbers, 5).SelectMany(arr => arr).ToArray();
                Backtracking bkt1 = new Backtracking(newSequence, newNumbers);
                Int64 posibilities = bkt1.FindSolutions();



                sum += posibilities;
            }

            Console.WriteLine(sum);

            return Task.CompletedTask;
        }


    }
}
