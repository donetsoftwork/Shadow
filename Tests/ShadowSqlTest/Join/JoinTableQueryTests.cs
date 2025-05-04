using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Simples;
using TestSupports;

namespace ShadowSqlTest.Join;

public class JoinTableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void ApplyLeft()
    {
        var query = SimpleTable.Use("Comments")
            .Join(SimpleTable.Use("Posts"))
            .Apply("PostId", "Id", (q, PostId, Id) => q.And(PostId.Equal(Id)))
            .ApplyLeft("Pick", (q, Pick) => q.And(Pick.EqualValue(true)));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1", sql);
    }
    [Fact]
    public void ApplyRight()
    {
        var query = SimpleTable.Use("Posts")
            .Join(SimpleTable.Use("Comments"))
            .Apply("Id", "PostId", (q, Id, PostId) => q.And(Id.Equal(PostId)))
            .ApplyRight("Pick", (q, Pick) => q.And(Pick.EqualValue(true)));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] WHERE t2.[Pick]=1", sql);
    }
    [Fact]
    public void Normal()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var query = c.Join(p)
            .And(c.PostId.Equal(p.Id))
            .Root
            .And(c.Pick.EqualValue(true))
            .And(p.Author.EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'", sql);
    }
    [Fact]
    public void TableName()
    {
        var joinOn = JoinOnQuery.Create("Comments", "Posts")
            .Apply("PostId", "Id", (logic, PostId, Id) => logic.And(PostId.Equal(Id)));
        JoinTableQuery query = joinOn.Root
            .Apply("t1", static (q, c) => q.And(c.Field("Pick").EqualValue(true)))
            .Apply("t2", static (q, p) => q.And(p.Field("Author").EqualValue("张三")));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1 AND t2.[Author]='张三'", sql);
    }
    [Fact]
    public void Apply()
    {
        var query = new CommentAliasTable("c")
            .Join(new PostAliasTable("p"))
            .Apply((q, c, p) => q.And(c.PostId.Equal(p.Id)))
            .Root
            .Apply<CommentAliasTable>("c", (q, c) => q.And(c.Pick.EqualValue(true)))
            .Apply<PostAliasTable>("p", (q, p) => q.And(p.Author.EqualValue("张三")));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1 AND p.[Author]='张三'", sql);
    }
}
