using System.Collections.Concurrent;

namespace AoC.Quest12
{
    internal class Quest12 : BaseQuest
    {
        Int64 sum = 0;
        public override Task Solve()
        {
            string inPath = GetPathTo("quest12_1.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = File.ReadAllLines(inPath);
            File.WriteAllText(outPath, "");

            int degreeOfParallelism = Environment.ProcessorCount * 3/4;

            Parallel.ForEach(
                Partitioner.Create(0, lines.Length),
                new ParallelOptions { MaxDegreeOfParallelism = degreeOfParallelism },
                range =>
                {
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        DoDynamicProgramming(lines[i],i,lines.Length);
                    }
                });


            Console.WriteLine(sum);
            File.WriteAllText(outPath, sum.ToString());

            return Task.CompletedTask;
        }

        private void DoDynamicProgramming(string line, int currentIndex, int length)
        {
            Console.WriteLine($"{currentIndex} / {length}");
            string[] splitArray = line.Split(' ');
            string sequence = splitArray[0];
            int[] numbers = splitArray[1].Split(',').Select(Int32.Parse).ToArray();
            string newSequence = string.Join("?", Enumerable.Repeat(sequence, 5));
            int[] newNumbers = Enumerable.Repeat(numbers, 5).SelectMany(arr => arr).ToArray();
            Backtracking bkt1 = new Backtracking(newSequence, newNumbers);
            Int64 posibilities = bkt1.FindSolutions();
            sum += posibilities;
        }


    }
}
