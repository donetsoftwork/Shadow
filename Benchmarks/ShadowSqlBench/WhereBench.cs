using BenchmarkDotNet.Attributes;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.Identifiers;
using SqlKata;
using SqlKata.Compilers;

namespace ShadowSqlBench;

//[MemoryDiagnoser, SimpleJob(launchCount: 4, warmupCount: 10, iterationCount: 10, invocationCount: 10000)]
[MemoryDiagnoser, SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 1, invocationCount: 1)]
public class WhereBench
{
    private static ISqlEngine _engine = new MySqlEngine();
    private static DB _db = DB.Use("ShadowSql");
    private static Compiler _compiler = new SqliteCompiler();
    private static IColumn Id = ShadowSql.Identifiers.Column.Use("Id");

    [Benchmark]
    public string ShadowSqlByFieldName()
    {
        var query = new Table("Posts")
            .ToQuery()
            .ColumnEqualValue("Id", 10)
            .ToSelect();
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark]
    public string ShadowSqlByParametricLogic()
    {
        var query = _db.From("Posts")
            .ToSelect(Id.EqualValue(10));
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark]
    public string ShadowSqlByLogic()
    {
        var query = _db.From("Posts")
            .ToSelect(Id.Equal());
        var sql = _engine.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark(Baseline = true)]
    public string SqlKataBench()
    {
        var query = new Query("Posts")
            .Where("Id", 10);
        var result = _compiler.Compile(query);
        var sql = result.Sql;
        Console.WriteLine(sql);
        return sql;
    }
}
