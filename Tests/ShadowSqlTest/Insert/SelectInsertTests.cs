using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
namespace ShadowSqlTest.Insert;

public class SelectInsertTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDB");

    [Fact]
    public void ToInsert()
    {
        var select = _db.From("Students")
            .ToSqlQuery()
            .Where("AddTime between '2024-01-01' and '2025-01-01'")
            .ToSelect()
            .Select("Name", "Age");
        var insert = _db.From("Backup2024")
            .ToInsert(select);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'", sql);
    }

    [Fact]
    public void InsertTo()
    {
        var insert = _db.From("Students")
            .ToSqlQuery()
            .Where("AddTime between '2024-01-01' and '2025-01-01'")
            .ToSelect()
            .Select("Name", "Age")
            .InsertTo(_db.From("Backup2024"));
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'", sql);
    }
    [Fact]
    public void InsertTo2()
    {
        var insert = _db.From("Students")
            .ToSqlQuery()
            .Where("AddTime between '2024-01-01' and '2025-01-01'")
            .ToSelect()
            .Select("Name", "Age")
            .InsertTo("Backup2024");
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'", sql);
    }
}
