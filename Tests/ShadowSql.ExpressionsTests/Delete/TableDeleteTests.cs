using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.Expressions.Tables;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Delete;

public class TableDeleteTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToDelete()
    {
        var delete = EmptyTable.Use("Posts")
            .ToDelete<Post>(p => p.Id == 1);
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Posts] WHERE [Id]=1", sql);
    }
    [Fact]
    public void ToDelete2()
    {
        var delete = EmptyTable.Use("Posts")
            .ToDelete<Post, Post>((u, p) => u.Id == p.Id);
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Posts] WHERE [Id]=@Id", sql);
    }
    [Fact]
    public void Query()
    {
        var delete = new TableSqlQuery<Student>("Students")
            .Where(s => s.Score < 60)
            .ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }
    [Fact]
    public void TableQuery()
    {
        var delete = new TableQuery<Student>()
            .And(s => s.Score < 60)
            .ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Student] WHERE [Score]<60", sql);
    }
}

