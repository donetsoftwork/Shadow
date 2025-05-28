using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Identifiers;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Visit;

public class OrderByVisitorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Member()
    {
        var table = EmptyTable.Use("Users");
        List<IOrderAsc> fields = [];
        var visitor = TableVisitor.OrderBy<User, int>(table, fields, u => u.Id);
        var cursor = table.ToCursor<User>();
        foreach (var field in visitor.Fields)
            cursor.Asc(field);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] ORDER BY [Id]", sql);
    }
    [Fact]
    public void Member2()
    {
        var table = EmptyTable.Use("Users");
        List<IOrderAsc> fields = [];
        var visitor = TableVisitor.OrderBy<User, object>(table, fields, u => new { u.Age, u.Id });
        var cursor = table.ToCursor<User>();
        foreach (var field in visitor.Fields)
            cursor.Desc(field);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] ORDER BY [Age] DESC,[Id] DESC", sql);
    }
}
