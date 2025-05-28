using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.Expressions.Tables;
using ShadowSql.ExpressionsTests.Supports;

namespace ShadowSql.ExpressionsTests.Delete;

public class TableDeleteTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

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

