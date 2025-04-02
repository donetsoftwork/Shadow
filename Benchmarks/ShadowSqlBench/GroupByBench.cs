using BenchmarkDotNet.Attributes;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.Identifiers;
using SqlKata;
using SqlKata.Compilers;

namespace ShadowSqlBench;

[MemoryDiagnoser, SimpleJob(launchCount: 4, warmupCount: 10, iterationCount: 10, invocationCount: 10000)]
//[MemoryDiagnoser, SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 1, invocationCount: 1)]
public class GroupByBench
{
    private static ISqlEngine _engine = new MySqlEngine();
    private static DB _db = DB.Use("ShadowSql");

    private static Compiler _compiler = new MySqlCompiler();
    private static IColumn PostId = ShadowSql.Identifiers.Column.Use("PostId");
    private static IColumn Category = ShadowSql.Identifiers.Column.Use("Category");
    private static IColumn Pick = ShadowSql.Identifiers.Column.Use("Pick");
    private static IColumn Hits = ShadowSql.Identifiers.Column.Use("Hits");
    private static Table Comments = _db.From("Comments")
        .AddColums(PostId, Category, Pick);

    [Benchmark]
    public string ShadowSqlBySqlQuery()
    {
        var query = new Table("Comments")
            .ToSqlQuery()
            .ColumnEqualValue("Category", "csharp")
            .ColumnEqualValue("Pick", true)
            .GroupBy("PostId")
            .HavingAggregate("SUM", "Hits", hits => hits.GreaterEqualValue(100))
            .ToFetch()
            .Desc(PostId)
            .ToSelect();
        query.Fields.Select("PostId")
            .SelectCount("Count");
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByQuery()
    {
        var query = new Table("Comments")
            .ToQuery()
            .And(Category.EqualValue("csharp"))
            .And(Pick.EqualValue(true))
            .GroupBy("PostId")
            .And(Hits.Sum().GreaterEqualValue(100))
            .ToFetch()
            .Desc(PostId)
            .ToSelect();
        query.Fields.Select("PostId")
            .SelectCount("Count");
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByParametricLogic()
    {
        var query = Comments
            .GroupBy(Category.EqualValue("csharp") & Pick.EqualValue(true), [PostId])
            .And(Hits.Sum().GreaterEqualValue(100))
            .ToFetch()
            .Desc(PostId)
            .ToSelect();
        query.Fields.Select(PostId)
            .SelectCount("Count");
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByLogic()
    {
        var query = Comments
            .GroupBy(Category.Equal().And(Pick.Equal()), [PostId])
            .And(Hits.Sum().GreaterEqual("Hits"))
            .ToFetch()
            .Desc(PostId)
            .ToSelect();
        query.Fields.Select(PostId)
            .SelectCount("Count");
        var sql = _engine.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark(Baseline = true)]
    public string SqlKataBench()
    {
        var query = new Query("Comments")
            .Where("Category", "csharp")
            .Where("Pick", true)
            .GroupBy("PostId")
            .Select("PostId")
            .SelectRaw("COUNT(1) as Count")
            .HavingRaw("SUM(Hits) >= 100")
            .OrderByDesc("PostId");
        var result = _compiler.Compile(query);
        var sql = result.Sql;
        //Console.WriteLine(sql);
        return sql;
    }
}
