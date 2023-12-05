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
    public class BenchmarkQuiz
    {

        //[Benchmark]
        //public void Benchmark1()
        //{
        //    Quest1 q = new Quest1();
        //    q.Solve();
        //}

        //[Benchmark]
        //public void Benchmark2()
        //{
        //    Quest2 q = new Quest2();
        //    q.Solve();
        //}

        //[Benchmark]
        public void Benchmark3()
        {
            Quest3 q = new Quest3();
            q.Solve();
        }

        [Benchmark]
        public void Benchmark4()
        {
            BaseQuest q = new Quest4();
            q.Solve();
        }
    }
}
