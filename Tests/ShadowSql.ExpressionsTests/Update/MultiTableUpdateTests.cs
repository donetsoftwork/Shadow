using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Join;
using ShadowSql.Tables;
using ShadowSql.Expressions;

namespace ShadowSql.ExpressionsTests.Update;

public class MultiTableUpdateTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToUpdate()
    {
        var joinOn = EmptyTable.Use("Comments")
            .As("c").SqlJoin<Comment, Post>(EmptyTable.Use("Posts").As("p"))
            .On((c, p) => c.PostId == p.Id)
            .WhereRight(p => p.Author == "张三")
            .WhereLeft(c => c.Pick == false);
        var update = joinOn.Root
            .ToUpdate()
            .Set<Comment>(c => new Comment { Pick = true });
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE c SET c.[Pick]=1 FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三' AND c.[Pick]=0", sql);
    }       
}
