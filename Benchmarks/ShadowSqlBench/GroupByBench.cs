using BenchmarkDotNet.Attributes;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using SqlKata;
using SqlKata.Compilers;

namespace ShadowSqlBench;

[MemoryDiagnoser, SimpleJob(launchCount: 2, warmupCount: 10, iterationCount: 10, invocationCount: 1000000)]
//[MemoryDiagnoser, SimpleJob(launchCount: 1, warmupCount: 0, iterationCount: 0, invocationCount: 1)]
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
    public string ShadowSqlByTableName()
    {
        var select = new Table("Comments")
            .ToSqlQuery()
            .FieldEqualValue("Category", "csharp")
            .FieldEqualValue("Pick", true)
            .SqlGroupBy("PostId")
            .HavingAggregate("SUM", "Hits", hits => hits.GreaterEqualValue(100))
            .ToCursor()
            .CountDesc()
            .ToSelect()
            .SelectGroupBy()
            .SelectCount("Count");
        ParametricContext context = new(_engine);
        var sql = context.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlBySqlQuery()
    {
        var select = Comments.ToSqlQuery()
            .Where(Category.EqualValue("csharp"))
            .Where(Pick.EqualValue(true))
            .SqlGroupBy(PostId)
            .Having(Hits.Sum().GreaterEqualValue(100))
            .ToCursor()
            .CountDesc()
            .ToSelect()
            .SelectGroupBy()
            .SelectCount("Count");
        var sql = _engine.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByLogic()
    {
        var select = Comments.GroupBy(Category.Equal().And(Pick.Equal()), PostId)
            .And(Hits.Sum().GreaterEqualValue(100))
            .ToCursor()
            .CountDesc()
            .ToSelect()
            .SelectGroupBy()
            .SelectCount("Count");
        var sql = _engine.Sql(select);
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
            .OrderByDesc("COUNT(1)");
        var result = _compiler.Compile(query);
        var sql = result.Sql;
        //Console.WriteLine(sql);
        return sql;
    }
}
