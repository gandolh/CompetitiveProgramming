using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
    [MemoryDiagnoser]
    [Config(typeof(AntiVirusFriendlyConfig))]
    public class BenchmarkQuiz1
    {

        [Benchmark]
        public void Benchmark1()
        {
            Quest1 q = new Quest1();
            q.Solve();
        }
    }
}
