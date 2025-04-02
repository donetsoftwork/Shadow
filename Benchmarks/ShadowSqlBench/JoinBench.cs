using BenchmarkDotNet.Attributes;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.Identifiers;
using ShadowSql.Variants;
using SqlKata;
using SqlKata.Compilers;
using Column = ShadowSql.Identifiers.Column;

namespace ShadowSqlBench;


[MemoryDiagnoser, SimpleJob(launchCount: 4, warmupCount: 10, iterationCount: 10, invocationCount: 10000)]
//[MemoryDiagnoser, SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 1, invocationCount: 1)]
public class JoinBench
{
    private static ISqlEngine _engine = new MySqlEngine();
    private static Compiler _compiler = new MySqlCompiler();
    private static CommentTable c = new("c");
    private static PostTable p = new("p");

    [Benchmark]
    public string ShadowSqlBySqlQuery()
    {
        var joinOn = new Table("Comments").As("c")
            .SqlJoin(new Table("Posts").As("p"))
            .OnColumn("PostId", "Id");

        var query = joinOn.Root
            .TableColumnEqualValue("c", "Pick", true)
            .TableColumnEqualValue("p", "Author", "jxj")
            .ToFetch()
             .Desc("c", c => c.Field("Id"))
             .ToSelect();
        query.Fields.Select("c", c => [c.Field("Id"), c.Field("Content")]);

        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByQuery()
    {
        var joinOn = new Table("Comments").As("c")
            .SqlJoin(new Table("Posts").As("p"))
            .OnColumn("PostId", "Id");

        var query = joinOn.Root
            .TableColumnEqualValue("c", "Pick", true)
            .TableColumnEqualValue("p", "Author", "jxj")
            .ToFetch()
             .Desc("c", c => c.Field("Id"))
             .ToSelect();
        query.Fields.Select("c", c => [c.Field("Id"), c.Field("Content")]);

        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByParametricLogic()
    {
        var joinOn = c.Join(p)
            .And(c.PostId.Equal(p.Id));
        var query = joinOn.Root
            .And(c.Pick.EqualValue(true))
            .And(p.Author.EqualValue("jxj"))
            .ToFetch()
            .Desc(c.Id)
            .ToSelect();
        query.Fields.Select(c.Id, c.Content);
        ParametricContext context = new(_engine);
        var sql = context.Sql(query);
        //Console.WriteLine(sql);
        return sql;
    }
    [Benchmark]
    public string ShadowSqlByLogic()
    {
        var joinOn = c.Join(p)
            .And(c.PostId.Equal(p.Id));
        var query = joinOn.Root
            .And(c.Pick.Equal())
            .And(p.Author.Equal())
            .ToFetch()
            .Desc(c.Id)
            .ToSelect();
        query.Fields.Select(c.Id, c.Content);
        var sql = _engine.Sql(query);
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
        public readonly IPrefixColumn Id;
        public readonly IPrefixColumn PostId;
        public readonly IPrefixColumn Content;
        public readonly IPrefixColumn Pick;
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
        public readonly IPrefixColumn Id;
        public readonly IPrefixColumn Title;
        public readonly IPrefixColumn Author;
    }

}
