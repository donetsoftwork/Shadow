using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using TestSupports;

namespace ShadowSqlCoreTest.Update;

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
            .Set(c.Pick.EqualToValue(true));
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
            .Set(c.Pick.EqualToValue(true));
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
            .Set(c.Pick.EqualToValue(true));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE c SET c.[Pick]=1 FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三' AND c.[Pick]=0", sql);
    }
    [Fact]
    public void UpdateTableName()
    {
        var joinOn = JoinOnSqlQuery.Create(new Table("Comments"), new Table("Posts"));
        var (t1, t2) = (joinOn.Left, joinOn.Source);
        joinOn.On(t1.Field("PostId").Equal(t2.Field("Id")));
        var query = joinOn.Root
            .Where(t2.Field("Author").EqualValue("张三"))
            .Where(t1.Field("Pick").EqualValue(false));
        var update = query.ToUpdate()
            .Update("Comments")
            .Set(t1.Field("Pick").EqualToValue(true));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE t1 SET t1.[Pick]=1 FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t2.[Author]='张三' AND t1.[Pick]=0", sql);
    }
}
