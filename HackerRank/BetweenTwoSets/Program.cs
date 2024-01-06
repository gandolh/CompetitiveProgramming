using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'getTotalX' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY a
     *  2. INTEGER_ARRAY b
     */

    private static int Cmmdc(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static int Cmmmc(int a, int b)
    {
        return a * b / Cmmdc(a, b);
    }

    
    public static int getTotalX(List<int> a, List<int> b)
    {
        int cmmmc = a[0];
        for (int i = 1; i < a.Count ; i++)
            cmmmc = Cmmmc(cmmmc, a[i]);

        //int cmmdc = b[0];
        //for (int i = 1; i < b.Count ; i++)
        //    cmmdc = Cmmdc(cmmdc, b[i]);

        //int nr = cmmdc / cmmmc;

        int nr = 0;
        int bMin = b.Min();
        for(int i=1; i*cmmmc <= bMin;i++)
            if(b.All( el => el% (i*cmmmc) == 0))
                nr++;
        return nr;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int m = Convert.ToInt32(firstMultipleInput[1]);

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        List<int> brr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(brrTemp => Convert.ToInt32(brrTemp)).ToList();

        int total = Result.getTotalX(arr, brr);

        Console.WriteLine(total);

        //textWriter.Flush();
        //textWriter.Close();
    }
}

