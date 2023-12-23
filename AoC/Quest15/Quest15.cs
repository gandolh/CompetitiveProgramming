using AoC.Quest13;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Quest15
{
    internal class Quest15 : BaseQuest
    {
        public override Task Solve()
        {
            string inPath = GetPathTo("quest15_1.in");
            string outPath = GetPathTo("questResult.out");
            File.WriteAllText(outPath, "");
            string[] lines = File.ReadAllLines(inPath);
            string fullseq= lines[0];

            string[] sequences = fullseq.Split(',');
            int totalSum = 0 ;
            foreach (string sequence in sequences)
            {
            
                int sum = GetHash(sequence);
                totalSum+= sum;
            }
            Console.WriteLine(totalSum);
            return Task.CompletedTask;
        }

        private int GetHash(string sequence)
        {
            int sum = 0;
            for (int i = 0; i < sequence.Length; i++)
            {
                sum = (sum + sequence[i]) * 17 % 256;
            }
            return sum;
        }
    }
}
