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
    static readonly IColumn name = Column.Use("Name");
    static readonly IColumn age = Column.Use("Age");

    [Fact]
    public void SelectInsert()
    {
        //IColumn name = Column.Use("Name");
        //IColumn age = Column.Use("Age");
        var query = new TableSqlQuery("Students")
            .Where("AddTime between '2024-01-01' and '2025-01-01'");
        var select = new TableSelect(query)
            .Select(name, age);
        var insert = new SelectInsert("Backup2024", select);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Backup2024]([Name],[Age])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'", sql);
    }
    [Fact]
    public void Insert()
    {
        //IColumn name = Column.Use("Name");
        //IColumn age = Column.Use("Age");
        IColumn name2 = Column.Use("Name2");
        IColumn age2 = Column.Use("Age2");
        var query = new TableSqlQuery("Students")
            .Where("AddTime between '2024-01-01' and '2025-01-01'");
        var select = new TableSelect(query)
            .Select(name, age);
        var insert = new SelectInsert("Backup2024", select)
            .Insert(name2, age2);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Backup2024]([Name2],[Age2])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'", sql);
    }

    [Fact]
    public void InsertColumnName()
    {
        //IColumn name = Column.Use("Name");
        //IColumn age = Column.Use("Age");
        var query = new TableSqlQuery("Students")
            .Where("AddTime between '2024-01-01' and '2025-01-01'");
        var select = new TableSelect(query)
            .Select(name, age);
        var insert = new SelectInsert("Backup2024", select)
            .Insert("Name2", "Age2");
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Backup2024]([Name2],[Age2])SELECT [Name],[Age] FROM [Students] WHERE AddTime between '2024-01-01' and '2025-01-01'", sql);
    }

}
