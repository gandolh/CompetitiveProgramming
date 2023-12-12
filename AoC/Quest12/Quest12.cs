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
            string inPath = GetPathTo("quest12_0.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = File.ReadAllLines(inPath);
            File.WriteAllText(outPath, "");
            int sum = 0;

            foreach (string line in lines)
            {
                string[] splitArray = line.Split(' ');
                string sequence = splitArray[0];
                int[] numbers = splitArray[1].Split(',').Select(Int32.Parse).ToArray();
                Backtracking bkt = new Backtracking(sequence, numbers);
                int numberOfSolutions = bkt.FindSolutions(sequence, numbers);
                sum+= numberOfSolutions;
            }

            Console.WriteLine(sum);

            return Task.CompletedTask;
        }

        
    }
}
