using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using TestSupports;

namespace ShadowSqlTest.Join;

public class MultiTableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void SqlMulti()
    {
        var multiTable = _db.From("Employees").Multi(_db.From("Departments"));
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Employees] AS t1,[Departments] AS t2", sql);
    }
    [Fact]
    public void SqlMulti2()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");

        var multiTable = c.Multi(p)
            .And(c.PostId.Equal(p.Id));
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id]", sql);
    }
    [Fact]
    public void TableName()
    {
        var multiTable = new MultiTableQuery()
            .AddMembers("Comments", "Posts");
        var t1 = multiTable.From("Comments");
        var t2 = multiTable.From("Posts");
        var query = multiTable.And(t1.Field("PostId").Equal(t2.Field("Id")))
            .Apply("t1", (q, c) => q.And(c.Field("Pick").EqualValue(true)))
            .Apply("t2", (q, p) => q.And(p.Field("Author").EqualValue("张三")));
        var sql = _engine.Sql(multiTable);
        Assert.Equal("[Comments] AS t1,[Posts] AS t2 WHERE t1.[PostId]=t2.[Id] AND t1.[Pick]=1 AND t2.[Author]='张三'", sql);
    }
    [Fact]
    public void Apply()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var query = c.Multi(p)
            .And(c.PostId.Equal(p.Id))
            .Apply<CommentAliasTable>("c", (q, c) => q.And(c.Pick.EqualValue(true)))
            .Apply<PostAliasTable>("p", (q, p) => q.And(p.Author.EqualValue("张三")));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id] AND c.[Pick]=1 AND p.[Author]='张三'", sql);
    }
}
