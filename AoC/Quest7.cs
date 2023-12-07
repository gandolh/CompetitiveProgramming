namespace AoC
{
    internal class Quest7 : BaseQuest
    {
        public enum HandType {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind
        }


        public override Task Solve()
        {
            string inPath = GetPathTo("quest7_1.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = System.IO.File.ReadAllLines(inPath);
            File.WriteAllText(outPath, "");

            Int64 totalWinning = 0;

            List<string> sortedLines = new List<string>(lines);
            sortedLines.Sort(handleSortByHand);

            for(int i = 0; i< sortedLines.Count; i++)
            {
                Int64 bid = Int64.Parse(sortedLines[i].Substring(6));
                totalWinning = totalWinning + bid * (i + 1);
            }

            File.WriteAllText(outPath, totalWinning.ToString());
            return Task.CompletedTask;

        }

        private int handleSortByHand(string x, string y)
        {
            (int fhm1, int fhm2) = getTwoMaximums(x[0..5]);
            (int shm1, int shm2) = getTwoMaximums(y[0..5]);

            HandType type1 = getHandType(fhm1, fhm2);
            HandType type2 = getHandType(shm1, shm2);
            if (type1 > type2) return 1;
            if( type1 < type2) return -1;
            if(type1 == type2)
            {
                for(int i = 0; i < 5; i++)
                {
                    int aVal = getCharValue(x[i]);
                    int bVal = getCharValue(y[i]);
                    if (aVal == bVal) continue;
                    if(aVal > bVal) return 1; 
                    else return -1;

                }
            }
            return 0;
        }


        private int getCharValue(char a)
        {
            int value = 0;
            if (a >= '0' && a <= '9') value = a - '0';
            if (a == 'T') value = 10;
            if (a == 'J') value = 11;
            if (a == 'Q') value = 12;
            if (a == 'K') value = 13;
            if (a == 'A') value = 14;
            return value;
        }

        private (int,int) getTwoMaximums(string hand)
        {
            int[] frequencyArray = new int[5];
            for(int i = 0; i < 5; i++)
            {
                frequencyArray[i] = hand.Count(x => x == hand[i]);
            }

            int max1 = -1;
            char max1Ch = '0';
            int max2 = -1;
            for(int i = 0;i< 5; i++)
            {
                if( frequencyArray[i] > max1)
                {
                    max2 = max1;
                    max1 = frequencyArray[i];
                    max1Ch = hand[i];
                }else if(frequencyArray[i] > max2 && hand[i] != max1Ch)
                {
                    max2 = frequencyArray[i];
                }
            }

            return (max1, max2);
        }

        
        private HandType getHandType(int max1, int max2)
        {
            if (max1 == 5) return HandType.FiveOfAKind;
            if (max1 == 4) return HandType.FourOfAKind;
            if (max1 == 3 && max2 == 2) return HandType.FullHouse;
            if (max1 == 3) return HandType.ThreeOfAKind;
            if (max1 == 2 && max2 == 2) return HandType.TwoPair;
            if(max1 == 2) return HandType.OnePair;
            if( max1 == 1) return HandType.HighCard;
            return HandType.HighCard;
        }
    }
}
