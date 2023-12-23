using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Quest16
{
    internal static class StringExtension
    {

        internal static char[][] ConvertToCharArray(this string[] stringsArray)
        {
            char[][] result = new char[stringsArray.Length][];

            for (int i = 0; i < stringsArray.Length; i++)
            {
                // Convert each string to a char array
                result[i] = stringsArray[i].ToCharArray();
            }

            return result;
        }
    }
}
