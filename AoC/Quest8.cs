using AoC.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AoC
{
    internal class Quest8 : BaseQuest
    {
        Dictionary<string, Vector2<string>> Maps = new();
        List<string> Locations = new List<string>();

        public override Task Solve()
        {
            string inPath = GetPathTo("quest8_1.in");
            string outPath = GetPathTo("questResult.out");
            string[] lines = System.IO.File.ReadAllLines(inPath);
            string destination = "ZZZ";
            File.WriteAllText(outPath, "");
            int[] directions = new int[lines[0].Length];

            for (int i = 0; i < lines[0].Length; i++)
                directions[i] = lines[0][i] == 'L' ? 0 : 1;

            for (int i = 2; i < lines.Length; i++)
            {
                string key = lines[i].Substring(0, 3);
                string toLeft = lines[i].Substring(7, 3);
                string toRight = lines[i].Substring(12, 3);
                Maps.Add(key, new Vector2<string>(toLeft, toRight));
                if (key.EndsWith('A')) Locations.Add(key);
            }


            int result = 0;
            MyTriple< string, long, long>[] zFindings = new MyTriple<string, long, long>[Locations.Count];
            for (int i = 0; i < Locations.Count; i++)
                zFindings[i] = new MyTriple<string, long, long>("",-1,-1);

            bool finishWhile = false;
            while (finishWhile == false)
            {
                for (int i = 0; i < Locations.Count; i++)
                {
                    Locations[i] =
                        Maps[Locations[i]][directions[result % directions.Length]];

                    if (Locations[i].EndsWith('Z'))
                    {
                        zFindings[i].First = Locations[i];
                        if(zFindings[i].Second == -1)
                            zFindings[i].Second = result + 1;
                        else if (zFindings[i].Third == -1)
                            zFindings[i].Third = result + 1;
                    }
                }

                result++;
                //allEndsWithZ = Locations.All(el => el.EndsWith('Z'));
                finishWhile = zFindings.All(el => el.Third >= 0);
            }

            Int64 finalResult = 1;
            for (int i = 0; i < zFindings.Length; i++)
                finalResult = LCM(finalResult, zFindings[i].Third - zFindings[i].Second);

            Console.WriteLine(finalResult);
            return Task.CompletedTask;

        }


        // greatest common divisor
        static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // least common multiple
        static long LCM(long a, long b)
        {
            return (a * b) / GCD(a, b);
        }



    }
}
