using BenchmarkDotNet.Attributes;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.Tables;
using ShadowSql.Expressions;
using ShadowSqlBench.Supports;
using SqlKata;
using SqlKata.Compilers;

namespace ShadowSqlBench;

[MemoryDiagnoser, SimpleJob(launchCount: 2, warmupCount: 10, iterationCount: 10, invocationCount: 2000000)]
//[MemoryDiagnoser, SimpleJob(launchCount: 1, warmupCount: 0, iterationCount: 0, invocationCount: 1)]
public class WhereBench
{
    private static readonly ISqlEngine _engine = new MySqlEngine();
    private static readonly ITable _table = EmptyTable.Use("Posts");
    private static readonly Compiler _compiler = new MySqlCompiler();

    [Benchmark]
    public string ShadowSqlByTableName()
    {
        var select = EmptyTable.Use("Posts")
            .ToSqlQuery()
            .FieldEqualValue("Id", 10)
            .ToSelect();
        ParametricContext context = new(_engine);
        var sql = context.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark]
    public string ShadowSqlBySqlQuery()
    {
        IColumn Id = ShadowSql.Identifiers.Column.Use("Id");
        var select = _table.ToSqlQuery()
            .Where(Id.EqualValue(10))
            .ToSelect();
        var sql = _engine.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByExpression()
    {
        int Id = 10;
        var select = _table.ToSelect<Post>(p => p.Id == Id);
        var sql = _engine.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByLogic()
    {
        IColumn Id = ShadowSql.Identifiers.Column.Use("Id");
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
