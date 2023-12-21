using AoC.Data;
using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using System.IO;

namespace AoC.Quest13
{
    internal class Quest13 : BaseQuest
    {
        private readonly Vector2<int> InvalidVector = new(-1, -1);

        public override Task Solve()
        {
            string inPath = GetPathTo("quest13_1.in");
            string outPath = GetPathTo("questResult.out");
            File.WriteAllText(outPath, "");

            string[][] patterns = GetPatterns(inPath);
            int colCount = 0;
            int rowCount = 0;
            for (int i = 0; i < patterns.Length; i++)
            {
                char[][] lines = patterns[i].ConvertToCharArray();
                int n = lines.Length;
                int m = lines[0].Length;
                CheckResult checkResRow = new CheckResult();
                CheckResult checkResCol = new CheckResult();
                int rowIndex = -1;
                int colIndex = -1;
                while ((checkResRow.SimetryFound == false || checkResRow.MismatchFound == false) && rowIndex < n)
                {
                    rowIndex = GetIdenticalRowIndex(lines, n, m, rowIndex + 1);
                    checkResRow = CheckRowsSymetry(lines, n, m, rowIndex);
                }
                while ((checkResCol.SimetryFound == false || checkResCol.MismatchFound == false) && colIndex < m)
                {
                    colIndex = GetIdenticalColIndex(lines, n, m, colIndex + 1);
                    checkResCol = CheckColsSymetry(lines, n, m, colIndex);
                }

                if (checkResCol.SimetryFound && checkResCol.MismatchFound)
                    colCount += colIndex + 1;
                if (checkResRow.SimetryFound && checkResRow.MismatchFound)
                    rowCount += rowIndex + 1;
            }


            Console.WriteLine(colCount + rowCount * 100);
            return Task.CompletedTask;
        }

        private void PrintLines(char[][] lines, int n, int m)
        {
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    Console.Write(lines[i][j]);
                }
                Console.WriteLine();
            }
        }

        private void FlipTheSmudge(char[][] lines, int x, int y)
        {
            char currentChar = lines[x][y];
            lines[x][y] = currentChar == '#' ? '.' : '#';
        }

        private CheckResult CheckColsSymetry(char[][] lines, int n, int m, int colIndex)
        {
            Vector2<int> myPos = new Vector2<int>(-1, -1);
            bool foundMissPlace = false;

            for (int i = colIndex, j = colIndex + 1; i >= 0 && j < m; i--, j++)
            {
                for (int k = 0; k < n; k++)
                {
                    if (lines[k][i] != lines[k][j])
                    {
                        if (!foundMissPlace)
                        {
                            foundMissPlace = true;
                            myPos = new Vector2<int>(k, i);
                        }
                        else return new CheckResult() { MismatchFound = false, SimetryFound = false, MissMatchPos = InvalidVector };
                    }
                }
            }

            return new CheckResult() { MismatchFound = foundMissPlace, SimetryFound = true, MissMatchPos = new(myPos) };
        }

        private CheckResult CheckRowsSymetry(char[][] lines, int n, int m, int rowIndex)
        {
            Vector2<int> myPos = new Vector2<int>(-1, -1);
            bool foundMissPlace = false;
            for (int i = rowIndex, j = rowIndex + 1; i >= 0 && j < n; i--, j++)
            {
                for (int k = 0; k < m; k++)
                {
                    if (lines[i][k] != lines[j][k])
                    {
                        if (!foundMissPlace)
                        {
                            foundMissPlace = true;
                            myPos = new Vector2<int>(i, k);
                        }
                        else return new CheckResult() { MismatchFound = false, SimetryFound = false, MissMatchPos = InvalidVector };
                    }

                }
            }

            return new CheckResult() { MismatchFound = foundMissPlace, SimetryFound = true, MissMatchPos = new(myPos) };
        }

        private string[][] GetPatterns(string inPath)
        {
            string[] lines = File.ReadAllLines(inPath);
            int k = 0;
            List<List<string>> patterns = [];
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    k++;
                    continue;
                }
                if (k == patterns.Count)
                    patterns.Add(new List<string>());
                patterns[k].Add(lines[i]);
            }
            return patterns.Select(el => el.ToArray()).ToArray();
        }

        private int GetIdenticalRowIndex(char[][] lines, int n, int m, int startRowIndex)
        {
            bool isIdenticalRow;
            for (int i = startRowIndex; i < n - 1; i++)
            {
                int missPlaceCount = 0;
                isIdenticalRow = true;
                for (int j = 0; j < m && isIdenticalRow; j++)
                {
                    if (lines[i][j] != lines[i + 1][j])
                    {
                        if (missPlaceCount != 0)
                            isIdenticalRow = false;
                        else
                        {
                            missPlaceCount++;
                        }
                    }
                }
                if (isIdenticalRow == true)
                    return i;
            }
            return n;
        }

        private int GetIdenticalColIndex(char[][] lines, int n, int m, int startColIndex)
        {
            bool isIdenticalCol;
            for (int i = startColIndex; i < m - 1; i++)
            {
                int missPlaceCount = 0;
                isIdenticalCol = true;
                for (int j = 0; j < n && isIdenticalCol; j++)
                {
                    if (lines[j][i] != lines[j][i + 1])
                    {
                        if (missPlaceCount != 0)
                            isIdenticalCol = false;
                        else
                        {
                            missPlaceCount++;

                        }
                        missPlaceCount++;
                    }
                }
                if (isIdenticalCol == true)
                    return i;
            }
            return m;
        }
    }
}
