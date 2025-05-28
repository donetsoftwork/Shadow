using BenchmarkDotNet.Attributes;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.Expressions;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Select;
using ShadowSql.Tables;
using ShadowSqlBench.Supports;
using SqlKata;
using SqlKata.Compilers;

namespace ShadowSqlBench;


[MemoryDiagnoser, SimpleJob(launchCount: 2, warmupCount: 10, iterationCount: 10, invocationCount: 1000000)]
//[MemoryDiagnoser, SimpleJob(launchCount: 1, warmupCount: 0, iterationCount: 0, invocationCount: 1)]
public class JoinBench
{
    private static readonly ISqlEngine _engine = new MySqlEngine();
    private static readonly Compiler _compiler = new MySqlCompiler();
    private static readonly CommentAliasTable c = new("c");
    private static readonly PostAliasTable p = new("p");

    [Benchmark]
    public string ShadowSqlByTableName()
    {
        var query = EmptyTable.Use("Comments").As("c")
            .SqlJoin(EmptyTable.Use("Posts").As("p"))
            .OnColumn("PostId", "Id")
            .Root
            .TableFieldEqualValue("c", "Pick", true)
            .TableFieldEqualValue("p", "Author", "jxj");
        // 由于引用命名空间的原因
        // 易用版和表达式版有冲突导致不能调用ToCursor连写
        var select = new ShadowSql.Cursors.MultiTableCursor(query)
            .Desc<IAliasTable>("c", c => c.Field("Id"))
            .ToSelect()
            .Select<IAliasTable>("c", c => [c.Field("Id"), c.Field("Content")]);

        ParametricContext context = new(_engine);
        var sql = context.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlBySqlQuery()
    {
        var query = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .Where(c.Pick.Equal())
            .Where(p.Author.Equal());
        // 由于引用命名空间的原因
        // 易用版和表达式版有冲突导致不能调用ToCursor连写
        var select = new ShadowSql.Cursors.MultiTableCursor(query)
            .Desc(c.Id)
            .ToSelect()
            .Select(c.Id, c.Content);

        var sql = _engine.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByLogic()
    {
        var joinOn = JoinOnQuery.Create(c, p)
            .And(c.PostId.Equal(p.Id));
        var query = joinOn.Root.And(c.Pick.Equal())
            .And(p.Author.Equal());
        var cursor = new ShadowSql.Cursors.TableCursor(query)
            .Desc(c.Id);
        var select = new TableSelect(cursor)
            .Select(c.Id, c.Content);
        var sql = _engine.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark]
    public string ShadowSqlByExpression()
    {
        bool Pick = true;
        string Author = "jxj";
        var joinOn = c.SqlJoin<Comment, Post>(p)
            .On((comment, post) => comment.PostId == post.Id)
            .WhereLeft(c => c.Pick == Pick)
            .WhereRight(p => p.Author == Author);
        // 由于引用命名空间的原因
        // 易用版和表达式版有冲突导致不能调用ToCursor连写
        var select = new ShadowSql.Expressions.Cursors.MultiTableCursor(joinOn.Root)
            .Desc<Comment, int>("c", c => c.Id)
            .ToSelect()
            .Select<Comment, object>("c", c => new { c.Id, c.Content });
        var sql = _engine.Sql(select);
        //Console.WriteLine(sql);
        return sql;
    }

    [Benchmark(Baseline = true)]
    public string SqlKataBench()
    {
        var query = new Query("Comments as c")
            .Join("Posts as p", "c.PostId", "p.Id")
            .Where("c.Pick", true)
            .Where("p.Author", "jxj")
            .OrderByDesc("c.Id")
            .Select("c.Id", "c.Content");
        var result = _compiler.Compile(query);
        var sql = result.Sql;
        //Console.WriteLine(sql);
        return sql;
    }
}
