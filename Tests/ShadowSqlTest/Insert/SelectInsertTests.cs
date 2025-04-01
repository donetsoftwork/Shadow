using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
namespace ShadowSqlTest.Insert;

public class SelectInsertTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDB");
    static readonly IColumn _name = Column.Use("Name");
    static readonly IColumn _age = Column.Use("Age");

    [Fact]
    public void ToInsert()
    {
        var select = _db.From("Students")
            .ToQuery()
            .Where("AddTime between '2024-01-01' and '2025-01-01'")
            .ToSelect();
        select.Fields.Select(_name, _age);
        var insert = _db.From("Backup2024")
            .AddColums(_name, _age)
            .ToInsert(select);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'", sql);
    }

    [Fact]
    public void InsertTo()
    {
        var backup = _db.From("Backup2024")
            .AddColums(_name, _age);
        var insert = _db.From("Students")
            .ToQuery()
            .Where("AddTime between '2024-01-01' and '2025-01-01'")
            .ToSelect()
            .Select(select => select.Fields.Select(_name, _age))
            .InsertTo(backup);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'", sql);
    }
}
