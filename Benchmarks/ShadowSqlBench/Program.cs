using BenchmarkDotNet.Running;
using ShadowSqlBench;

//WhereBench bench = new();
//bench.ShadowSqlByExpression();

BenchmarkRunner.Run<WhereBench>();
//BenchmarkRunner.Run<CursorBench>();
//BenchmarkRunner.Run<GroupByBench>();
//BenchmarkRunner.Run<JoinBench>();

partial class Program { }