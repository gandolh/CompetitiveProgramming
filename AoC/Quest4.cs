using System.Text.RegularExpressions;

namespace AoC
{
    internal class Quest4 : BaseQuest
    {
        private Regex _regex = new Regex(" +");

        public override Task Solve()
        {
            string inPath = GetPathTo("quest5_0.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = System.IO.File.ReadAllLines(inPath);
            int sum = 0;
            File.WriteAllText(outPath, "");
            // indexing from 1. (blasphemy)
            int[] instancesOfCard = new int[lines.Length + 1];

            for (int i = 0; i < lines.Length; i++)
            {
                instancesOfCard[i + 1] += 1;

                string[] splitArray = lines[i].Split(':');
                (_, string scratchCards) = (splitArray[0], splitArray[1]);
                splitArray = scratchCards.Split("|");
                (string myNumbersStr, string myWinningNumbersStr) = (splitArray[0].Trim(), splitArray[1].Trim());
                int winCardsCount = 0;

                List<int> myNumbers = ToIntList(myNumbersStr);
                List<int> myWinningNumbers = ToIntList(myWinningNumbersStr);
                foreach (var num in myNumbers)
                {
                    if (myWinningNumbers.Contains(num))
                        winCardsCount++;
                }

                if (winCardsCount != 0)
                {
                    for (int j = 1; j <= winCardsCount; j++)
                    {
                        instancesOfCard[i + j + 1] = instancesOfCard[i + j + 1] + instancesOfCard[i + 1];
                    }
                }
            }

            sum = instancesOfCard.Aggregate((el1, el2) => el1 + el2);
            File.WriteAllText(outPath, sum.ToString());
            return Task.CompletedTask;
        }


        // numbers in str separated by " ". Convert to int[]
        private List<int> ToIntList(string numbers)
        {
            return _regex.Split(numbers).Select((el) => Int32.Parse(el)).ToList();
        }
    }
}
