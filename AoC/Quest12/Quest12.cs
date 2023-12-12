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
                Backtracking bkt1 = new Backtracking(sequence, numbers);
                Backtracking bkt2 = new Backtracking("?" + sequence, numbers);
                Backtracking bkt3 = new Backtracking(sequence + "?", numbers);
                Int64 posibilities = 0;
                Int64 solutionWithoutAdding = bkt1.FindSolutions();
                Int64 solutions1 = bkt2.FindSolutions();
                Int64 solutions2 = bkt3.FindSolutions();
                if (sequence.EndsWith("#"))
                {
                    posibilities = solutionWithoutAdding * solutions2
                        * solutions2 * solutions2 * solutions2;
                }
                else
                {
                    Int64 noSolution = Math.Max(solutions1, solutions2);
                    posibilities = solutionWithoutAdding * noSolution *
                        noSolution * noSolution * noSolution;
                }

                Console.WriteLine("====" + posibilities);
                sum += posibilities;
            }

            Console.WriteLine(sum);

            return Task.CompletedTask;
        }


    }
}
