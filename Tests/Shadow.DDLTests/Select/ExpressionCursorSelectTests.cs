using Shadow.DDL.Schemas;
using Shadow.DDLTests.Supports;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using System.Text;

namespace Shadow.DDLTests.Select;

public class ExpressionCursorSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    [Fact]
    public void Filter()
    {
        var select = new TableSchema("Users", [], "tenant1")
            .ToSqlQuery<User>()
            .Where(u => u.Status)
            .Take(10, 20)
            .Desc(u => u.Id)
            .ToSelect();
        var sql = new StringBuilder();
        _engine.SelectCursor(sql, select, select.Source);
        Assert.Equal("SELECT * FROM [tenant1].[Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql.ToString());
    }
}
