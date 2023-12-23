namespace AoC.Quest15
{
    internal class Quest15 : BaseQuest
    {
        readonly Dictionary<int, List<BoxLens>> Boxes = [];

        public override Task Solve()
        {
            string inPath = GetPathTo("quest15_1.in");
            string outPath = GetPathTo("questResult.out");
            File.WriteAllText(outPath, "");
            string[] lines = File.ReadAllLines(inPath);
            string fullseq= lines[0];

            string[] sequences = fullseq.Split(',');
            string label;
            string[] splitArray;
            for (int i = 0; i < 256; i++)
                Boxes.Add(i, new());

            foreach (string sequence in sequences)
            {
                if (sequence.Contains('='))
                {
                    splitArray = sequence.Split('=');
                    label = splitArray[0];
                    int value = Int32.Parse(splitArray[1]);
                    int hash = GetHash(label);
                    int index = Boxes[hash].FindIndex(el => el.Label == label);
                    if (index != -1)
                        Boxes[hash][index].Value = value;
                    else Boxes[hash].Add(new BoxLens(label,value));
                }
                else
                {
                    splitArray = sequence.Split('-');
                    label = splitArray[0];
                    int hash = GetHash(label);
                    int index = Boxes[hash].FindIndex(el => el.Label == label);
                    if(index!= -1)
                    Boxes[hash].RemoveAt(index);
                }
            }

            int totalSum = 0;
            for(int i=0;i<256; i++)
            {
                
                for (int j=0;j< Boxes[i].Count;j++)
                {
                     totalSum = totalSum +  (i+1) * (j+1) * Boxes[i][j].Value;
                }
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
