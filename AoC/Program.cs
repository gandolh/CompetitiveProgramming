// See https://aka.ms/new-console-template for more information
using AoC;
using BenchmarkDotNet.Running;


Quest1 q = new Quest1();
q.Solve();

var summary = BenchmarkRunner.Run<BenchmarkQuiz1>();