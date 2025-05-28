using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Join;
using TestSupports;

namespace ShadowSqlTest.Update;

public class MultiTableUpdateTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToUpdate()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnSqlQuery.Create(c, p)
            .On(c.PostId.Equal(p.Id));
        var query = joinOn.Root
            .Where(p.Author.EqualValue("张三"))
            .Where(c.Pick.EqualValue(false));
        var update = query.ToUpdate()
            .Set(c.Pick.AssignValue(true));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE c SET c.[Pick]=1 FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三' AND c.[Pick]=0", sql);
    }
    [Fact]
    public void Query()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnQuery.Create(c, p)
            .And(c.PostId.Equal(p.Id));
        var query = joinOn.Root
            .And(p.Author.EqualValue("张三"))
            .And(c.Pick.EqualValue(false));
        var update = query.ToUpdate()
            .Set(c.Pick.AssignValue(true));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE c SET c.[Pick]=1 FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三' AND c.[Pick]=0", sql);
    }
    [Fact]
    public void Update()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var joinOn = JoinOnSqlQuery.Create(c, p)
            .On(c.PostId.Equal(p.Id));
        var query = joinOn.Root
            .Where(p.Author.EqualValue("张三"))
            .Where(c.Pick.EqualValue(false));
        var update = query.ToUpdate()
            .Update(c)
            .Set(c.Pick.AssignValue(true));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE c SET c.[Pick]=1 FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三' AND c.[Pick]=0", sql);
    }    
}
