using BenchmarkDotNet.Attributes;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using ShadowSql.Expressions;
using ShadowSql.Select;
using ShadowSql.Tables;
using ShadowSqlBench.Supports;
using SqlKata;
using SqlKata.Compilers;

namespace ShadowSqlBench;

[MemoryDiagnoser, SimpleJob(launchCount: 2, warmupCount: 10, iterationCount: 10, invocationCount: 500000)]
//[MemoryDiagnoser, SimpleJob(launchCount: 1, warmupCount: 0, iterationCount: 0, invocationCount: 1)]
public class CursorBench
{
    private static readonly ISqlEngine _engine = new MySqlEngine();
    private static readonly DB _db = DB.Use("ShadowSql");
    private static readonly Table _table = _db.From("Posts");
    private static readonly Compiler _compiler = new MySqlCompiler();
    private static readonly IColumn Id = ShadowSql.Identifiers.Column.Use("Id");
    private static readonly IColumn Title = ShadowSql.Identifiers.Column.Use("Title");
    private static readonly IColumn Category = ShadowSql.Identifiers.Column.Use("Category");
    private static readonly IColumn Pick = ShadowSql.Identifiers.Column.Use("Pick");

    [Benchmark]
    public string ShadowSqlByTableName()
    {
        var select = EmptyTable.Use("Posts")
            .ToSqlQuery()
            .FieldEqualValue("Category", "csharp")
            .FieldEqualValue("Pick", true)
            .ToCursor()
            .Skip(10)
            .Take(10)
            .Desc(Id)
            .ToSelect()
            .Select("Id", "Title");
        ParametricContext context = new(_engine);
        var sql = context.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlBySqlQuery()
    {
        var select = _table.ToSqlQuery()
            .Where(Category.EqualValue("csharp"))
            .Where(Pick.EqualValue(true))
            .ToCursor(10, 10)
            .Desc(Id)
            .ToSelect()
            .Select(Id, Title);
        var sql = _engine.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark]
    public string ShadowSqlByExpression()
    {
        bool Pick = true;
        var select = _table.ToQuery<Post>()
            .And(p => p.Pick == Pick)
            .Take(10, 10)
            .Desc(p => p.Id)
            .ToSelect()
            .Select(p => new { p.Id, p.Title });
        var sql = _engine.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark]
    public string ShadowSqlByLogic()
    {
        var filter = new TableFilter(_table, Category.Equal().And(Pick.Equal()));
        var cursor = new TableCursor(filter, 10, 10)
            .Desc(Id);
        var select = new TableSelect(cursor)
            .Select(Id, Title);
        var sql = _engine.Sql(select);
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
