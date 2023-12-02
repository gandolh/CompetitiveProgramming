// See https://aka.ms/new-console-template for more information
using AoC;





DateTime start = DateTime.Now;
Quest1 q = new Quest1();
q.Solve();
DateTime end = DateTime.Now;

Console.WriteLine(end - start);
