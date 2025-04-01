using BenchmarkDotNet.Running;
using ShadowSqlBench;

BenchmarkRunner.Run<WhereBench>();
//BenchmarkRunner.Run<FetchBench>();
//BenchmarkRunner.Run<GroupByBench>();
//BenchmarkRunner.Run<JoinBench>();

partial class Program { }