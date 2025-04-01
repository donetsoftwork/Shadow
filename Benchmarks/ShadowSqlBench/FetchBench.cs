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
public class FetchBench
{
    private static ISqlEngine _engine = new MySqlEngine();
    private static DB _db = DB.Use("ShadowSql");
    private static Compiler _compiler = new MySqlCompiler();
    private static IColumn Id = ShadowSql.Identifiers.Column.Use("Id");
    private static IColumn Title = ShadowSql.Identifiers.Column.Use("Title");
    private static IColumn Category = ShadowSql.Identifiers.Column.Use("Category");
    private static IColumn Pick = ShadowSql.Identifiers.Column.Use("Pick");

    [Benchmark]
    public string ShadowSqlBySqlQuery()
    {
        var query = new Table("Posts")
            .ToSqlQuery()
            .ColumnEqualValue("Category", "csharp")
            .ColumnEqualValue("Pick", true)
            .ToFetch()
            .Skip(10)
            .Take(10)
            .Desc(Id)
            .ToSelect();
        query.Fields.Select("Id", "Title");
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
            .ColumnEqualValue("Category", "csharp")
            .ColumnEqualValue("Pick", true)
            .ToFetch()
            .Skip(10)
            .Take(10)
            .Desc(Id)
            .ToSelect();
        query.Fields.Select("Id", "Title");
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByParametricLogic()
    {
        var query = _db.From("Posts")
            .ToFetch(Category.EqualValue("csharp") & Pick.EqualValue(true), 10, 10)
            .Desc(Id)
            .ToSelect()
            .Select(select => select.Fields.Select(Id, Title));
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByLogic()
    {
        var query = _db.From("Posts")
            .ToFetch(Category.Equal().And(Pick.Equal()), 10, 10)
            .Desc(Id)
            .ToSelect();
        query.Fields.Select(Id, Title);
        var sql = _engine.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark(Baseline = true)]
    public string SqlKataBench()
    {
        var query = new Query("Posts")
            .Where("Category", "csharp")
            .Where("Pick", true)
            .Offset(10)
            .Limit(10)
            .OrderByDesc("Id")
            .Select("Id", "Title");
        var result = _compiler.Compile(query);
        var sql = result.Sql;
        //Console.WriteLine(sql);
        return sql;
    }
}
