using BenchmarkDotNet.Attributes;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.Tables;
using SqlKata;
using SqlKata.Compilers;

namespace ShadowSqlBench;

[MemoryDiagnoser, SimpleJob(launchCount: 4, warmupCount: 10, iterationCount: 10, invocationCount: 10000)]
//[MemoryDiagnoser, SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 1, invocationCount: 1)]
public class WhereBench
{
    private static ISqlEngine _engine = new MySqlEngine();
    private static DB _db = DB.Use("ShadowSql");
    private static Table _table = _db.From("Posts");
    private static Compiler _compiler = new MySqlCompiler();
    private static IColumn Id = ShadowSql.Identifiers.Column.Use("Id");

    [Benchmark]
    public string ShadowSqlBySqlQuery()
    {
        var query = new Table("Posts")
            .ToSqlQuery()
            .ColumnEqualValue("Id", 10)
            .ToSelect();
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark]
    public string ShadowSqlByQuery()
    {
        var query = new Table("Posts")
            .ToQuery()
            .And(Id.EqualValue(10))
            .ToSelect();
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark]
    public string ShadowSqlByParametricLogic()
    {
        var filter = new TableFilter(_table, Id.EqualValue(10));
        var select = new TableSelect(filter);
        ParametricContext context = new(_engine);
        var sql = context.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark]
    public string ShadowSqlByLogic()
    {
        var filter = new TableFilter(_table, Id.Equal());
        var select = new TableSelect(filter);
        var sql = _engine.Sql(select);
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
        //Console.WriteLine(sql);
        return sql;
    }
}
