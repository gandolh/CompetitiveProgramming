// See https://aka.ms/new-console-template for more information
using AoC;
using BenchmarkDotNet.Running;


//BaseQuest q = new Quest1();
//BaseQuest q = new Quest2();
//BaseQuest q = new Quest3();
//BaseQuest q = new Quest4();
//BaseQuest q = new Quest5();
//BaseQuest q = new Quest6();
//BaseQuest q = new Quest7();
BaseQuest q = new Quest8();
await q.Solve();

//var summary = BenchmarkRunner.Run<BenchmarkQuiz>();