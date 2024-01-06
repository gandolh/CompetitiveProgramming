
class Result
{

    /*
     * Complete the 'kangaroo' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts following parameters:
     *  1. INTEGER x1
     *  2. INTEGER v1
     *  3. INTEGER x2
     *  4. INTEGER v2
     */

    public static string kangaroo(int x1, int v1, int x2, int v2)
    {
        #region NAIVE
        //while (x1 != x2) {
        //    if (x2 > x1 && v2 > v1) return "NO";
        //    if (x1 > x2 && v1 > v2) return "NO";
        //    x1 += v1;       
        //    x2 += v2;       
        //}
        //return "YES";
        #endregion END NAIVE
        long speedDifference = v1 - v2;
        long startDifference = x2 - x1;
        if(speedDifference == 0)
        {
            return startDifference == 0 ? "YES" : "NO";
        }
 
        long steps = startDifference / speedDifference;
        long newX1 = x1 + v1 * steps;
        long newX2 = x2 + v2 * steps;
        if (newX1 == newX2 && steps > 0)
            return "YES";
        return "NO";
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int x1 = Convert.ToInt32(firstMultipleInput[0]);

        int v1 = Convert.ToInt32(firstMultipleInput[1]);

        int x2 = Convert.ToInt32(firstMultipleInput[2]);

        int v2 = Convert.ToInt32(firstMultipleInput[3]);

        string result = Result.kangaroo(x1, v1, x2, v2);

        Console.WriteLine(result);

        //textWriter.Flush();
        //textWriter.Close();
    }
}
