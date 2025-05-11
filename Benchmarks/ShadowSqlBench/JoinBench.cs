using BenchmarkDotNet.Attributes;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Select;
using ShadowSql.Variants;
using SqlKata;
using SqlKata.Compilers;
using Column = ShadowSql.Identifiers.Column;

namespace ShadowSqlBench;


[MemoryDiagnoser, SimpleJob(launchCount: 2, warmupCount: 10, iterationCount: 10, invocationCount: 1000000)]
//[MemoryDiagnoser, SimpleJob(launchCount: 1, warmupCount: 0, iterationCount: 0, invocationCount: 1)]
public class JoinBench
{
    private static ISqlEngine _engine = new MySqlEngine();
    private static Compiler _compiler = new MySqlCompiler();
    private static CommentTable c = new("c");
    private static PostTable p = new("p");

    [Benchmark]
    public string ShadowSqlByTableName()
    {
        var select = new Table("Comments").As("c")
            .SqlJoin(new Table("Posts").As("p"))
            .OnColumn("PostId", "Id")
            .Root
            .TableFieldEqualValue("c", "Pick", true)
            .TableFieldEqualValue("p", "Author", "jxj")
            .ToCursor()
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
        var select = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .Where(c.Pick.Equal())
            .Where(p.Author.Equal())
            .ToCursor()
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
        var cursor = new TableCursor(query)
            .Desc(c.Id);
        var select = new TableSelect(cursor)
            .Select(c.Id, c.Content);
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

    class CommentTable : TableAlias<Table>
    {
        public CommentTable(string tableAlias)
            : this(new Table("Comments"), tableAlias)
        {
        }
        private CommentTable(Table table, string tableAlias)
            : base(table, tableAlias)
        {
            Id = AddColumn(Column.Use(nameof(Id)));
            PostId = AddColumn(Column.Use(nameof(PostId)));
            Content = AddColumn(Column.Use(nameof(Content)));
            Pick = AddColumn(Column.Use(nameof(Pick)));
        }
        public readonly IPrefixField Id;
        public readonly IPrefixField PostId;
        public readonly IPrefixField Content;
        public readonly IPrefixField Pick;
    }
    class PostTable : TableAlias<Table>
    {
        public PostTable(string tableAlias)
            : this(new Table("Posts"), tableAlias)
        {
        }
        private PostTable(Table table, string tableAlias)
            : base(table, tableAlias)
        {
            Id = AddColumn(Column.Use(nameof(Id)));
            Title = AddColumn(Column.Use(nameof(Title)));
            Author = AddColumn(Column.Use(nameof(Author)));
        }
        //Id, Title, Author
        public readonly IPrefixField Id;
        public readonly IPrefixField Title;
        public readonly IPrefixField Author;
    }

}
