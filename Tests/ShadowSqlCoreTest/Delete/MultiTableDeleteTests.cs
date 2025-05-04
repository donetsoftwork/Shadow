using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Join;
using TestSupports;

namespace ShadowSqlCoreTest.Delete;

public class MultiTableDeleteTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Query()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnQuery.Create(c, p)
            .And(c.PostId.Equal(p.Id));
        var query = joinOn.Root
            .And(p.Author.EqualValue("张三"));
        var delete = query.ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三'", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnSqlQuery.Create(c, p)
            .On(c.PostId, p.Id);
        var query = joinOn.Root
            .Where(p.Author.EqualValue("张三"));
        var delete = query.ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三'", sql);
    }
    [Fact]
    public void Delete()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnSqlQuery.Create(c, p)
            .On(c.PostId, p.Id);
        var query = joinOn.Root
            .Where(p.Author.EqualValue("张三"));
        var delete = query.ToDelete()
            .Delete(c);
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三'", sql);
    }
}
