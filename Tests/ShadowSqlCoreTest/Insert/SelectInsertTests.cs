using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Select;
using ShadowSql.Tables;

namespace ShadowSqlCoreTest.Insert;

public class SelectInsertTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDB");
    static readonly IColumn _name = Column.Use("Name");
    static readonly IColumn _age = Column.Use("Age");

    [Fact]
    public void ToInsert()
    {
        var backup = _db.From("Backup2024")
            .AddColums(_name, _age);
        var query = new TableSqlQuery("Students")
            .Where("AddTime between '2024-01-01' and '2025-01-01'");
        var select = new TableSelect(query)
            .Select(_name, _age);
        var insert = new SelectInsert(backup, select);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'", sql);
    }

}
