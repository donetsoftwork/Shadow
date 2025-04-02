using BenchmarkDotNet.Running;
using ShadowSqlBench;

//BenchmarkRunner.Run<WhereBench>();
BenchmarkRunner.Run<CursorBench>();
//BenchmarkRunner.Run<GroupByBench>();
//BenchmarkRunner.Run<JoinBench>();

partial class Program { }