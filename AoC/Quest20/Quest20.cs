using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Quest20
{
    internal class Quest20 : BaseQuest
    {
        public override Task Solve()
        {
            string inPath = GetPathTo("quest19_0.in");
            string outPath = GetPathTo("questResult.out");
            File.WriteAllText(outPath, "");
            string[] lines = File.ReadAllLines(inPath);
            foreach (string line in lines)
            {

            }
            return Task.CompletedTask;
        }
    }
}
